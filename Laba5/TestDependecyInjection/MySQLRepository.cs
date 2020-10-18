using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class MySQLRepository: IRepository
    {
        public string SendRequest(string request)
        {
            return "MySQL request";
        }
    }
}
