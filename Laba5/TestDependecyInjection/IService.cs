using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    interface IService<TRepository> where TRepository: IRepository
    {
        string UseRepository(TRepository repository);
        string UseLocalRepository();
    }
}
