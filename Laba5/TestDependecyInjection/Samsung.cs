using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class Samsung: IPhone
    {
        string name;
        public string Name { get { return name; } }

        public Samsung(IRepository repository)
        {
            name = "Samsung Galaxy A40";
        }
    }
}
