using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Tracert
{
    public class Tracer: ITracer
    {
        object lockTrace = new Object();

        private bool isStarted;
        private long startTime, endTime;
        private TraceResult result;
        private ThreadRuntimeInfo threadInfo = new ThreadRuntimeInfo() 
            { Id = 1, EllapsedTime = 0, Methods = new List<MethodRuntimeInfo>()};
        private List<MethodRuntimeInfo> currentMethodList;
        private Stack<List<MethodRuntimeInfo>> methodsLists;
        private Stack<long> startTimes;
        private int currentThreadId;

        Dictionary<int, ThreadHelpInfo> threadsHelpInfo;

        public Tracer()
        {
            result = new TraceResult();
            currentThreadId = Thread.CurrentThread.ManagedThreadId;
            threadInfo.Id = currentThreadId;
            result.Threads = new List<ThreadRuntimeInfo>{ threadInfo};
            methodsLists = new Stack<List<MethodRuntimeInfo>>();
            startTimes = new Stack<long>();
            currentMethodList = threadInfo.Methods;
            methodsLists.Push(currentMethodList);
            threadsHelpInfo = new Dictionary<int, ThreadHelpInfo>();
            threadsHelpInfo.Add(currentThreadId, new ThreadHelpInfo(methodsLists, currentMethodList, 
                startTimes, isStarted, currentThreadId));
        }

        public void StartTrace() 
        {
            lock (lockTrace)
            {
                long oldStartTime = startTime;
                startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                int realThreadId;
                if ((realThreadId = Thread.CurrentThread.ManagedThreadId) != currentThreadId)
                {
                    if (threadsHelpInfo.ContainsKey(realThreadId))
                    {
                        threadsHelpInfo[currentThreadId] = new ThreadHelpInfo(methodsLists,
                            currentMethodList, startTimes, isStarted, currentThreadId);
                        currentThreadId = realThreadId;
                        ThreadHelpInfo currentThreadInfo = threadsHelpInfo[currentThreadId];
                        methodsLists = currentThreadInfo.methodsLists;
                        currentMethodList = currentThreadInfo.currentMethodList;
                        startTimes = currentThreadInfo.startTimes;
                        isStarted = currentThreadInfo.isStarted;
                        int i = 0;
                        while (result.Threads[i].Id != currentThreadId)
                            i++;
                        threadInfo = result.Threads[i];
                    }
                    else
                    {
                        threadsHelpInfo[currentThreadId] = new ThreadHelpInfo(methodsLists,
                            currentMethodList, startTimes, isStarted, currentThreadId);
                        currentThreadId = realThreadId;

                        //Initializing new thread
                        threadInfo = new ThreadRuntimeInfo()
                        {
                            Id = currentThreadId,
                            EllapsedTime = 0,
                            Methods = new List<MethodRuntimeInfo>()
                        };
                        currentMethodList = threadInfo.Methods;
                        methodsLists = new Stack<List<MethodRuntimeInfo>>();
                        methodsLists.Push(currentMethodList);
                        startTimes = new Stack<long>();
                        isStarted = false;
                        result.Threads.Add(threadInfo);
                        //-------------------------------

                        threadsHelpInfo.Add(currentThreadId, new ThreadHelpInfo(methodsLists,
                            currentMethodList, startTimes, isStarted, currentThreadId));
                    }
                }
                if (isStarted)
                {
                    //--------------------------
                    //---------------------------
                   // threadsHelpInfo[oldStartTime.ThreadID].startTimes.Push(oldStartTime);
                    startTimes.Push(oldStartTime);
                    methodsLists.Push(currentMethodList);
                    currentMethodList.Add(new MethodRuntimeInfo());
                    currentMethodList = currentMethodList[currentMethodList.Count - 1].Methods;
                }
                isStarted = true;
            }
        }

        public void StopTrace()
        {
            lock (lockTrace)
            {
                StackFrame frame = new StackFrame(1);
                var method = frame.GetMethod();
                string methodName = method.Name;
                string className = method.DeclaringType.Name;
                endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                if (Thread.CurrentThread.ManagedThreadId != currentThreadId)
                {
                    threadsHelpInfo[currentThreadId] = new ThreadHelpInfo(methodsLists,
                        currentMethodList, startTimes, isStarted, currentThreadId);
                    currentThreadId = Thread.CurrentThread.ManagedThreadId;
                    ThreadHelpInfo currentThreadInfo = threadsHelpInfo[currentThreadId];
                    methodsLists = currentThreadInfo.methodsLists;
                    currentMethodList = currentThreadInfo.currentMethodList;
                    startTimes = currentThreadInfo.startTimes;
                    isStarted = currentThreadInfo.isStarted;
                    int i = 0;
                    while (result.Threads[i].Id != currentThreadId)
                        i++;
                    threadInfo = result.Threads[i];
                }
                if (!isStarted)
                {
                    startTime = startTimes.Pop();
                    currentMethodList = methodsLists.Pop();
                    currentMethodList[currentMethodList.Count - 1].EllapsedTime = endTime - startTime;
                    currentMethodList[currentMethodList.Count - 1].ClassName = className;
                    currentMethodList[currentMethodList.Count - 1].MethodName = methodName;
                }
                else
                {
                    currentMethodList.Add(new MethodRuntimeInfo()
                    {
                        EllapsedTime =
                        endTime - startTime,
                        ClassName = className,
                        MethodName = methodName
                    });
                    isStarted = false;

                }
            }
        } 

        public TraceResult GetTraceResult()
        {
            long time;
            for(int i  = 0; i < result.Threads.Count; i++)
            {
                time = 0;
                foreach(var method in result.Threads[i].Methods)
                {
                    time += method.EllapsedTime;
                }
                result.Threads[i].EllapsedTime = time;
            }
            return result;
        }

    }
}
