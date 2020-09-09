using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracert
{
    struct StartTime
    {
        public long Time;
        public int ThreadID;

        public StartTime(long time, int threadID)
        {
            Time = time;
            ThreadID = threadID;
        }
    }
}
