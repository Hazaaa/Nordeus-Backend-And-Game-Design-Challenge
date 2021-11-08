using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// Region could be set when user is logged in.
        /// </summary>
        public Region Region { get; set; }

        public Club Club { get; set; }
    }
}
