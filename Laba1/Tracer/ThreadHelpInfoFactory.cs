using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracert
{
    class ThreadHelpInfoFactory
    {

        public static ThreadHelpInfo getInstance(Stack<List<MethodRuntimeInfo>> methodsLists,
            List<MethodRuntimeInfo> currentMethodList, Stack<long> startTimes,
            bool isStarted, int threadID)
        {
            return new ThreadHelpInfo(methodsLists, currentMethodList, startTimes, isStarted, threadID);
        }

    }
}
