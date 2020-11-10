using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class SmsMessage: IMessage
    {
        int counter = 0;
        public int Counter { get { return counter; } }

        public string Send()
        {
            counter++;
            return "Message from sms";
        }

    }
}
