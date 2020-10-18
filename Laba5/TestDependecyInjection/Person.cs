using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class Person
    {
        int age;
        public int Age { get { return age; } }

        public Person(int age)
        {
            this.age = age;
        }
    }
}
