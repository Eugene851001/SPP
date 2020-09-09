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
        public bool isStarted;
        public int threadID;

        public ThreadHelpInfo(Stack<List<MethodRuntimeInfo>> methodsLists, 
            List<MethodRuntimeInfo> currentMethodList, Stack<long> startTimes, 
            bool isStarted, int threadID)
        {
            this.currentMethodList = currentMethodList;
            this.methodsLists = methodsLists;
            this.startTimes = startTimes;
            this.isStarted = isStarted;
            this.threadID = threadID;
        }
    }
}
