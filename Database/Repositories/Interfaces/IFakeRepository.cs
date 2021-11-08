using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Repositories.Interfaces
{
    public interface IFakeRepository<T>
    {
        List<T> Get();

        T GetById();

        T Update(int id);

        bool Delete(int id);
    }
}
