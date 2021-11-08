using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Manager
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public int ClubId { get; set; }

        // public Club Club { get; set; }

        /// <summary>
        /// Manager rating also affects the strength of the club.
        /// </summary>
        public int Rating { get; set; }

        /// <summary>
        /// Every manager has their favorite formation.
        /// </summary>
        public string FavoriteFormation { get; set; }
    }
}
