using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Tracert
{
    public class Tracer: ITracer
    {

        private bool isStarted;
        private long startTime, endTime;
        private long threadStartTime;
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
            threadStartTime = long.MaxValue;
            threadsHelpInfo = new Dictionary<int, ThreadHelpInfo>();
            threadsHelpInfo.Add(currentThreadId, new ThreadHelpInfo(methodsLists, currentMethodList, 
                startTimes, threadStartTime, isStarted));
        }

        public void StartTrace()
        {
            startTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if (startTime < threadStartTime)
                threadStartTime = startTime;
            if (Thread.CurrentThread.ManagedThreadId != currentThreadId)
            {
                if(threadsHelpInfo.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                {
                    threadsHelpInfo[currentThreadId] = new ThreadHelpInfo(methodsLists, 
                        currentMethodList, startTimes, threadStartTime, isStarted);
                    currentThreadId = Thread.CurrentThread.ManagedThreadId;
                    ThreadHelpInfo currentThreadInfo = threadsHelpInfo[currentThreadId];
                    methodsLists = currentThreadInfo.methodsLists;
                    currentMethodList = currentThreadInfo.currentMethodList;
                    startTimes = currentThreadInfo.startTimes;
                    isStarted = currentThreadInfo.isStarted;
                    threadStartTime = currentThreadInfo.threadStartTime;
                }
                else
                {
                    threadsHelpInfo[currentThreadId] = new ThreadHelpInfo(methodsLists, 
                        currentMethodList, startTimes, threadStartTime, isStarted);
                    currentThreadId = Thread.CurrentThread.ManagedThreadId;
                    
                    //Initializing new thread
                    threadInfo = new ThreadRuntimeInfo() { Id = currentThreadId, 
                        EllapsedTime = 0, Methods = new List<MethodRuntimeInfo>()};
                    currentMethodList = threadInfo.Methods;
                    methodsLists = new Stack<List<MethodRuntimeInfo>>();
                    methodsLists.Push(currentMethodList);
                    isStarted = false;
                    result.Threads.Add(threadInfo);
                    threadStartTime = startTime;
                    //-------------------------------

                    //constructor parameters does not matter
                    threadsHelpInfo.Add(currentThreadId, new ThreadHelpInfo(methodsLists, 
                        currentMethodList, startTimes, threadStartTime, isStarted));
                }
            }
            if (isStarted)
            {
                startTimes.Push(startTime);
                methodsLists.Push(currentMethodList);
                currentMethodList.Add(new MethodRuntimeInfo());
                currentMethodList = currentMethodList[currentMethodList.Count - 1].Methods;
            }
            isStarted = true;
        }

        public void StopTrace()
        {
            StackFrame frame = new StackFrame(1);
            var method = frame.GetMethod();
            string methodName = method.Name;
            string className = method.DeclaringType.Name;
            endTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            if(Thread.CurrentThread.ManagedThreadId != currentThreadId)
            {
                threadsHelpInfo[currentThreadId] = new ThreadHelpInfo(methodsLists, 
                    currentMethodList, startTimes, threadStartTime, isStarted);
                currentThreadId = Thread.CurrentThread.ManagedThreadId;
                ThreadHelpInfo currentThreadInfo = threadsHelpInfo[currentThreadId];
                methodsLists = currentThreadInfo.methodsLists;
                currentMethodList = currentThreadInfo.currentMethodList;
                startTimes = currentThreadInfo.startTimes;
                isStarted = currentThreadInfo.isStarted;
                threadStartTime = currentThreadInfo.threadStartTime;
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
            threadInfo.EllapsedTime = endTime - threadStartTime;
        } 

        public TraceResult GetTraceResult()
        {
            return result;
        }

    }
}
