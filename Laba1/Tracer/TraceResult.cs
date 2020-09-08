using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracert
{
    public class TraceResult
    {

        public List<ThreadRuntimeInfo> Threads { get; set; }

        public TraceResult()
        {
            Threads = new List<ThreadRuntimeInfo>();
        }
    }
}
