using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class MongoDB: IMongoDB
    {
        public string SendRequest(string request)
        {
            return "MongoDB request";
        }

        public string MongoFunction()
        {
            return "MongoDB function";
        }
    }
}
