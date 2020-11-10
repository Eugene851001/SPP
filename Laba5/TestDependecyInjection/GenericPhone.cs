using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class GenericPhone<T>: IPhone
    {
        public string Name { get; }
        public List<T> Users;

        public GenericPhone()
        {
            Users = new List<T>();
            Name = "Generic Phone";
        }

    }
}
