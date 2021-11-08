using Common.Models;
using Database.Repositories.Interfaces;
using Database.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories
{
    public class FakeUserRepository : IFakeUserRepository
    {
        private List<User> _users;

        public FakeUserRepository()
        {
            _users = MockingDataService.MockCompleteUsers();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> Get()
        {
            return _users;
        }

        /// <inheritdoc/>
        public List<User> GetUsersByClubIds(List<int> clubIds)
        {
            return _users.Where(user => clubIds.Contains(user.Club.Id)).ToList();
        }

        public User GetById()
        {
            throw new NotImplementedException();
        }

        public User Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
