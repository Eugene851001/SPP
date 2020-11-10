using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DependencyInjectionContainer;

namespace TestDependecyInjection
{
    class Yandex<TRepository>: IService<TRepository> where TRepository: IRepository
    {
        TRepository repository;

        public Yandex([DependencyKey(ImplementationName.First)]TRepository repository)
        {
            this.repository = repository;
        }

        public string UseRepository(TRepository repository)
        {
            return "Yandex: " + repository.SendRequest("SELECT * FROM users"); 
        }

        public string UseLocalRepository()
        {
            return "Yandex: " + repository.SendRequest("");
        }
    }
}
