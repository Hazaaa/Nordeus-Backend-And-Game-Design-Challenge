using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    /// <summary>
    /// Service used for matchmaing players.
    /// </summary>
    public interface IMatchmakingService
    {
        /// <summary>
        /// Doing matchmaking for players in given clubs ids pool.
        /// </summary>
        /// <param name="clubsIdsPool">Ids of clubs in pool.</param>
        /// <returns>List of matches with home team id and away team id.</returns>
        List<MatchmakingResult> DoMatchmaking(List<int> clubsIdsPool);
    }
}
