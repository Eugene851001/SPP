using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class MailMessage: IMessage
    {
        int counter;
        public int Counter { get { return counter; } }

        public string Send()
        {
            counter++;
            return "Mail message";
        }
    }
}
