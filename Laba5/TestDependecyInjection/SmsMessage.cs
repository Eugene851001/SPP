using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDependecyInjection
{
    class SmsMessage: IMessage
    {
        public IPhone Phone;

        int counter = 0;
        public int Counter { get { return counter; } }

        public string Send()
        {
            counter++;
            return "Message from sms";
        }

        public SmsMessage(IPhone phone)
        {
            Phone = phone;
        }

    }
}
