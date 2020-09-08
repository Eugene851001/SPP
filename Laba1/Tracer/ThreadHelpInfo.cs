using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracert
{
    class ThreadHelpInfo
    {
        public List<MethodRuntimeInfo> currentMethodList;
        public Stack<List<MethodRuntimeInfo>> methodsLists;
        public Stack<long> startTimes;
        public long threadStartTime;
        public bool isStarted;

        public ThreadHelpInfo(Stack<List<MethodRuntimeInfo>> methodsLists, 
            List<MethodRuntimeInfo> currentMethodList, Stack<long> startTimes, 
            long threadStartTime, bool isStarted)
        {
            this.currentMethodList = currentMethodList;
            this.methodsLists = methodsLists;
            this.startTimes = startTimes;
            this.isStarted = isStarted;
            this.threadStartTime = threadStartTime;
        }
    }
}
