using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories.Interfaces
{
    public interface IFakeUserRepository : IFakeRepository<User>
    {
        /// <summary>
        /// Retrieving users by their clubs ids.
        /// </summary>
        /// <param name="clubIds">List of club ids.</param>
        /// <returns>List of users with their clubs.</returns>
        List<User> GetUsersByClubIds(List<int> clubIds);
    }
}
