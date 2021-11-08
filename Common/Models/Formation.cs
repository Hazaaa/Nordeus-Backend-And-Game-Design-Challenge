using Common.Constants;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    /// <summary>
    /// Class used for storing formation of the club
    /// </summary>
    public class Formation
    {
        /// <summary>
        /// Formation is type of two dimensional array and it will store players id that will be on specific position.
        /// </summary>
        private int[,] _startingFormation;

        /// <summary>
        /// Number of players currently in formation.
        /// </summary>
        private int _playersCountInFormation;

        /// <summary>
        /// Players can also be placed on bench but their rating is used with percent.
        /// </summary>
        private List<int> _bench;

        public Formation()
        {
            _startingFormation = new int[FormationConstants.FormationMatrixSize, FormationConstants.FormationMatrixSize];
            InitializeFormationMatrix();
            _playersCountInFormation = 0;
            _bench = new();
        }

        public Formation(int[,] formation, int playersCountInFormation, List<int> bench)
        {
            _startingFormation = formation;
            _playersCountInFormation = playersCountInFormation;
            _bench = bench;
        }

        /// <summary>
        /// Formation Code (e.g. 4-4-2)
        /// </summary>
        public string Code
        {
            get
            {
                StringBuilder formationName = new();

                for (int i = FormationConstants.FormationMatrixSize - 2; i >= 0; i--)
                {
                    int countInRow = 0;
                    for (int j = 0; j < FormationConstants.FormationMatrixSize; j++)
                    {
                        if (_startingFormation[i, j] != 0)
                        {
                            countInRow++;
                        }
                    }
                    if (countInRow != 0)
                    {
                        formationName.Append($"{countInRow}{(i == 0 ? "" : "-")}");
                    }
                }

                return formationName.ToString();
            }
        }

        /// <summary>
        /// Sets player to specific location in formation. If players number in formation is 11 and player is tried to set on empty position nothing will happen.
        /// </summary>
        /// <param name="row">Row position.</param>
        /// <param name="column">Column position.</param>
        /// <param name="playerId">Player id to set.</param>
        public void SetPlayerToPosition(int row, int column, int playerId)
        {
            if (_startingFormation[row, column] != 0)
            {
                _startingFormation[row, column] = playerId;
            }
            else
            {
                if (_playersCountInFormation == FormationConstants.MaxPlayersInFormationCount)
                {
                    return;
                }

                _startingFormation[row, column] = playerId;
                _playersCountInFormation++;
            }
        }

        /// <summary>
        /// Sets player on bench.
        /// </summary>
        /// <param name="playerId">Player id to set on bench.</param>
        public void SetPlayerToBench(int playerId)
        {
            if (_bench.Count == FormationConstants.MaxPlayersOnBenchCount)
            {
                return;
            }

            _bench.Add(playerId);
        }

        /// <summary>
        /// Removes player from specific location in formation.
        /// </summary>
        /// <param name="row">Row position.</param>
        /// <param name="column">Column position.</param>
        public void RemovePlayerFromPosition(int row, int column)
        {
            if (_playersCountInFormation > 0 && _startingFormation[row, column] != 0)
            {
                _playersCountInFormation--;
                _startingFormation[row, column] = 0;
            }
        }

        /// <summary>
        /// Removes player from the bench.
        /// </summary>
        /// <param name="playerId">Player id to remove from the bench.</param>
        public void RemovePlayerFromBench(int playerId)
        {
            _bench.Remove(playerId);
        }

        /// <summary>
        /// Checks if player is in starting formation.
        /// </summary>
        /// <param name="playerId">Player id</param>
        /// <returns>Player location in formation, otherwise -1,-1.</returns>
        public (int row, int column) IsPlayerInStartingFormation(int playerId)
        {
            for (int i = 0; i < FormationConstants.FormationMatrixSize; i++)
            {
                for (int j = 0; j < FormationConstants.FormationMatrixSize; j++)
                {
                    if (_startingFormation[i, j] == playerId)
                    {
                        return (i, j);
                    }
                }
            }

            return (-1, -1);
        }

        /// <summary>
        /// Checks if player is on the bench.
        /// </summary>
        /// <param name="playerId">Id of the player that is checked.</param>
        /// <returns>True if player is on the bench, otherwise false.</returns>
        public bool IsPlayerOnTheBench(int playerId)
        {
            return _bench.Any(plId => plId == playerId);
        }

        /// <summary>
        /// Checks whether player position is in correct location.
        /// </summary>
        /// <param name="row">Position row.</param>
        /// <param name="column">Position column.</param>
        /// <param name="playerPosition">Player position.</param>
        /// <returns>True if player position is in right location, otherwise false.</returns>
        public bool CheckIfPlayerPositionIsInRightLocation(int row, int column, PlayerPosition playerPosition)
        {
            bool gotPosition = FormationConstants.PlayerPositionsLocations.TryGetValue(playerPosition, out List<(int, int)> positions);

            if (!gotPosition)
            {
                return false;
            }

            return positions.Contains((row, column));
        }

        /// <summary>
        /// Initialize formation matrix to 0.
        /// </summary>
        private void InitializeFormationMatrix()
        {
            for (int i = 0; i < FormationConstants.FormationMatrixSize; i++)
            {
                for (int j = 0; j < FormationConstants.FormationMatrixSize; j++)
                {
                    _startingFormation[i, j] = 0;
                }
            }
        }
    }

    /// <summary>
    /// DTO used just for deserializing mocked data from JSON.
    /// </summary>
    public record FormationDTO(int ClubId, int[,] Formation, List<int> Bench);
}
