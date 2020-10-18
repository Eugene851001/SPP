using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class Yandex<TRepository>: IService<TRepository> where TRepository: IRepository
    {

        public string UseRepository(TRepository repository)
        {
            return "Yandex: " + repository.SendRequest("SELECT * FROM users"); 
        }

        public string UseLocalRepository()
        {
            return "Yandex has no local repository";
        }
    }
}
