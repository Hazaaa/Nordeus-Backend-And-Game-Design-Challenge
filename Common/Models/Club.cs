using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Club
    {
        public int Id { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// Id of user.
        /// </summary>
        public int OwnerId { get; set; }

        // public User Owner { get; set; }

        public Manager Manager { get; set; }

        public List<Player> Players { get; set; }

        public Formation Formation { get; set; }

        public Club()
        {
            Players = new List<Player>();
        }

        /// <summary>
        /// Gets power of the club that is calculated based on skills of the players, are players on right position in formation, managers skill and if their formation is used.
        /// </summary>
        public int Power => CalculatePowerOfTheClub();

        /// <summary>
        /// Calculates power of the club.
        /// </summary>
        /// <returns></returns>
        private int CalculatePowerOfTheClub()
        {
            int power = 0;

            foreach (Player player in Players)
            {
                (int row, int column) = Formation.IsPlayerInStartingFormation(player.Id);

                // If Player is in formation and on right position his rating is added to power of the club.
                if (row != -1 && column != -1)
                {
                    bool isInRightPosition = Formation.CheckIfPlayerPositionIsInRightLocation(row, column, player.Position);

                    if (isInRightPosition)
                    {
                        power += player.Rating;
                    }
                    else
                    {
                        // If player is in formation but on wrong position, 30% of his rating is added to power of the club.
                        power += (player.Rating * 30) / 100;
                    }
                }
                else
                {
                    bool isOnTheBench = Formation.IsPlayerOnTheBench(player.Id);

                    // If Player is on the bench his rating/skill is halfed and added to power of the team.
                    if (isOnTheBench)
                    {
                        power += player.Rating / 2;
                    }
                }
            }

            // If Manager favorite formation is used his rating is fully added to power of the club, if not half of his rating is added.
            if (Manager.FavoriteFormation == Formation.Code)
            {
                power += Manager.Rating;
            }
            else
            {
                power += Manager.Rating / 2;
            }

            return power;
        }
    }
}
