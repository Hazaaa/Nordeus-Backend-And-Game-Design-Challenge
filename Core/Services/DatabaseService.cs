using Core.Interfaces;
using Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IFakeUserRepository _fakeUserRepository;
        private readonly IFakeClubRepository _fakeClubRepository;
        private readonly IFakePlayerRepository _fakePlayerRepository;
        private readonly IFakeManagerRepository _fakeManagerRepository;

        public DatabaseService(IFakeUserRepository fakeUserRepository,
            IFakeClubRepository fakeClubRepository,
            IFakePlayerRepository fakePlayerRepository,
            IFakeManagerRepository fakeManagerRepository)
        {
            _fakeClubRepository = fakeClubRepository;
            _fakePlayerRepository = fakePlayerRepository;
            _fakeUserRepository = fakeUserRepository;
            _fakeManagerRepository = fakeManagerRepository;
        }

        public IFakeUserRepository FakeUserRepository => _fakeUserRepository;

        public IFakeClubRepository FakeClubRepository => _fakeClubRepository;

        public IFakePlayerRepository FakePlayerRepository => _fakePlayerRepository;

        public IFakeManagerRepository FakeManagerRepository => _fakeManagerRepository;
    }
}
