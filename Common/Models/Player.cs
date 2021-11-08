using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class Player
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        /// <summary>
        /// Player position that is best for him.
        /// </summary>
        public PlayerPosition Position { get; set; }

        /// <summary>
        /// Rating shows how skilled player is.
        /// </summary>
        public int Rating { get; set; }

        public int ClubId { get; set; }

        // public Club Club { get; set; }

        public string Worth { get; set; }
    }
}
