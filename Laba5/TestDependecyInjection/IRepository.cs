using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    interface IRepository
    {
        string SendRequest(string request);
    }
}
