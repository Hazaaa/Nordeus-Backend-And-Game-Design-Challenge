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
    public class FakeClubRepository : IFakeClubRepository
    {
        private List<Club> _clubs;

        public FakeClubRepository()
        {
            _clubs = MockingDataService.MockClubs();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Club> Get()
        {
            return _clubs;
        }

        public Club GetById()
        {
            throw new NotImplementedException();
        }

        public Club Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
