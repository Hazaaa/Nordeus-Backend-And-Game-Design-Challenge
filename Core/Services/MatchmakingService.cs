using Common.Constants;
using Common.Enums;
using Common.Models;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    /// <summary>
    /// Service used for matchmaing players.
    /// </summary>
    public class MatchmakingService : IMatchmakingService
    {
        private readonly IDatabaseService _databaseService;

        public MatchmakingService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        /// <summary>
        /// Implementation of matchmaking
        /// IDEA:
        /// We recieve even number of clubs ids in pool.
        /// First sort clubs in tiers by their power.
        /// (There could be many more tiers but for this simple implementation we have only 2 tiers Lower and Upper)
        /// Power <= 1000 => Lower tier
        /// Power > 1000 => Upper tier
        /// After that if there is enough clubs in tier, random match those clubs by region where their owner is. (Idea is that users has better playing experience if they are in same region on same server so latency can't be problem)
        /// If there's not enough clubs in same region then we match clubs by nearest region.
        /// Also if one club remaining in upper tier and lower tier they will be matched together. (Lucky winner)
        /// Higher power club gets home advantage.
        /// </summary>
        /// <param name="clubsIdsPool">Clubs ids in the pool.</param>
        /// <returns>List of matches.</returns>
        public List<MatchmakingResult> DoMatchmaking(List<int> clubsIdsPool)
        {
            List<MatchmakingResult> results = new();

            if (clubsIdsPool.Count == 0 || clubsIdsPool.Count == 1)
            {
                throw new ArgumentException("Not enough clubs for matchmaking!");
            }

            List<User> users = _databaseService.FakeUserRepository.GetUsersByClubIds(clubsIdsPool);
            List<Club> clubs = users.Select(user => user.Club).ToList();

            Task<(List<MatchmakingResult> lowerTierMatchmaking, Club remainingLowerClub)> lowerTierTask = new(() =>
            {
                // Sorting clubs to tiers by their power
                List<Club> lowerTier = clubs.Where(club => club.Power <= 1000).ToList();

                // Group clubs by region of their owner and sorting by club power
                var groupedLowerTier = lowerTier.GroupBy(club => users.First(user => user.Id == club.OwnerId).Region);

                return MatchmakingByTierAndRegion(groupedLowerTier);
            });

            Task<(List<MatchmakingResult> upperTierMatchmaking, Club remainingUpperClub)> upperTierTask = new(() =>
            {
                // Sorting clubs to tiers by their power
                List<Club> upperTier = clubs.Where(club => club.Power > 1000).ToList();

                // Group clubs by region of their owner and sorting by club power
                var groupedUpperTier = upperTier.GroupBy(club => users.First(user => user.Id == club.OwnerId).Region);

                return MatchmakingByTierAndRegion(groupedUpperTier);
            });

            lowerTierTask.Start();
            upperTierTask.Start();

            Task.WaitAll(lowerTierTask, upperTierTask);

            results.AddRange(lowerTierTask.Result.lowerTierMatchmaking);
            results.AddRange(upperTierTask.Result.upperTierMatchmaking);

            if (lowerTierTask.Result.remainingLowerClub != null && upperTierTask.Result.remainingUpperClub != null)
            {
                results.Add(new MatchmakingResult() { HomeTeamId = upperTierTask.Result.remainingUpperClub.Id, AwayTeamId = lowerTierTask.Result.remainingLowerClub.Id });
            }

            return results;
        }

        /// <summary>
        /// Matchmaking clubs by tier and region.
        /// </summary>
        /// <param name="tier">Tier</param>
        /// <returns>List of matched clubs and remaining club if number of remaining clubs are odd, otherwise is null.</returns>
        private (List<MatchmakingResult>, Club) MatchmakingByTierAndRegion(IEnumerable<IGrouping<Region, Club>> tier)
        {
            List<MatchmakingResult> matchmakingResults = new();
            Dictionary<Region, Club> remainingClubs = new();

            Random rnd = new();

            // Idea of random matching.
            // For every region there is list of clubs with equaly matched power, and from that list we get random two clubs.
            // Club with higher power has home adventage.
            // If there is odd number of clubs remaining clubs per region will be also random matched by nearest region.
            foreach (var region in tier)
            {
                List<Club> clubs = region.ToList();

                bool areEvenNumOfClubs = clubs.Count % 2 == 0;

                while (clubs.Count != (areEvenNumOfClubs ? 0 : 1))
                {
                    int firstRandomClubIndex = rnd.Next(0, clubs.Count - 1);
                    Club firstClub = clubs[firstRandomClubIndex];

                    clubs.RemoveAt(firstRandomClubIndex);

                    int secondRandomClubIndex = rnd.Next(0, clubs.Count - 1);
                    Club secondClub = clubs[secondRandomClubIndex];

                    clubs.RemoveAt(secondRandomClubIndex);

                    int homeTeamId;
                    int awayTeamId;
                    if (firstClub.Power >= secondClub.Power)
                    {
                        homeTeamId = firstClub.Id;
                        awayTeamId = secondClub.Id;
                    }
                    else
                    {
                        homeTeamId = secondClub.Id;
                        awayTeamId = firstClub.Id;
                    }

                    MatchmakingResult newMatch = new()
                    {
                        HomeTeamId = homeTeamId,
                        AwayTeamId = awayTeamId
                    };

                    matchmakingResults.Add(newMatch);
                }

                // If there is odd num of clubs in region one club will be left so we add it to remaining clubs.
                if (!areEvenNumOfClubs)
                {
                    remainingClubs.Add(region.Key, clubs.First());
                }
            }

            // Remaining clubs by each region are matched by nearest region to them
            (List<MatchmakingResult> matchesByNearestRegion, Club remainingClub) = MatchmakingForRemainingClubs(remainingClubs);

            matchmakingResults.AddRange(matchesByNearestRegion);

            return (matchmakingResults, remainingClub);
        }

        /// <summary>
        /// Does matchmaking for remaining clubs based on nearest region of the club.
        /// </summary>
        /// <param name="remainingClubs">Remaining clubs as dictionary where key is region in which club is.</param>
        /// <returns>List of matched clubs and remaining club if number of remaining clubs are odd, otherwise is null.</returns>
        private (List<MatchmakingResult>, Club) MatchmakingForRemainingClubs(Dictionary<Region, Club> remainingClubs)
        {
            List<MatchmakingResult> matches = new();
            Dictionary<Region, Club> tmpRemainingClubs = new(remainingClubs);
            List<int> matchedClubs = new();

            Club remainingClub = null;

            foreach (KeyValuePair<Region, Club> item in remainingClubs)
            {
                tmpRemainingClubs.Remove(item.Key);

                if (matchedClubs.Contains(item.Value.Id))
                {
                    continue;
                }

                Club firstClub = item.Value;
                Club secondClub = null;

                RegionConstants.NearestRegionsForRegion.TryGetValue(item.Key, out List<Region> nearestRegions);

                foreach (Region nearRegion in nearestRegions)
                {
                    tmpRemainingClubs.TryGetValue(nearRegion, out secondClub);

                    if (secondClub != null)
                    {
                        tmpRemainingClubs.Remove(nearRegion);
                        break;
                    }
                }

                if (secondClub != null)
                {
                    int homeTeamId;
                    int awayTeamId;

                    if (firstClub.Power >= secondClub.Power)
                    {
                        homeTeamId = firstClub.Id;
                        awayTeamId = secondClub.Id;
                    }
                    else
                    {
                        homeTeamId = secondClub.Id;
                        awayTeamId = firstClub.Id;
                    }

                    MatchmakingResult newMatch = new()
                    {
                        HomeTeamId = homeTeamId,
                        AwayTeamId = awayTeamId
                    };

                    matchedClubs.Add(homeTeamId);
                    matchedClubs.Add(awayTeamId);
                    matches.Add(newMatch);
                }
                else
                {
                    remainingClub = item.Value;
                }
            }


            return (matches, remainingClub);
        }
    }
}
