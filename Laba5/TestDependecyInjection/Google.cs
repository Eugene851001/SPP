using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class Google<TRepository>: IService<TRepository> where TRepository: IRepository
    {
        TRepository repository;

        public Google(TRepository repository)
        {
            this.repository = repository;
        }

        public string UseRepository(TRepository repository)
        {
            return "Google: " + repository.SendRequest("SELECT * FROM users");
        }

        public string UseLocalRepository()
        {
            return "Google: " + repository.SendRequest("request");
        }
    }
}
