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
    public class FakeManagerRepository : IFakeManagerRepository
    {
        private List<Manager> _managers;

        public FakeManagerRepository()
        {
            _managers = MockingDataService.MockManagers();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Manager> Get()
        {
            return _managers;
        }

        public Manager GetById()
        {
            throw new NotImplementedException();
        }

        public Manager Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
