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
    public class FakePlayerRepository : IFakePlayerRepository
    {
        private List<Player> _players;

        public FakePlayerRepository()
        {
            _players = MockingDataService.MockPlayers();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Player> Get()
        {
            return _players;
        }

        public Player GetById()
        {
            throw new NotImplementedException();
        }

        public Player Update(int id)
        {
            throw new NotImplementedException();
        }
    }
}
