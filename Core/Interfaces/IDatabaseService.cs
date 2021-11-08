using Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IDatabaseService
    {
        IFakeUserRepository FakeUserRepository { get; }

        IFakeClubRepository FakeClubRepository { get; }

        IFakePlayerRepository FakePlayerRepository { get; }

        IFakeManagerRepository FakeManagerRepository { get; }
    }
}
