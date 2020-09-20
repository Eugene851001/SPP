using System;
using Tracert;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestTracer
{
    [TestClass]
    public class TestTracer
    {

        static bool isResultReady;
        static ITracer sharedTracer;

        [TestMethod]
        public void resultMethodName()
        {
            Tracer tracer = new Tracer();

            tracer.StartTrace();
            tracer.StopTrace();

            Assert.AreEqual("resultMethodName", tracer.GetTraceResult().Threads[0].Methods[0].MethodName);
        }

        [TestMethod]
        public void resultClassName()
        {
            Tracer tracer = new Tracer();

            tracer.StartTrace();
            tracer.StopTrace();

            Assert.AreEqual("TestTracer", tracer.GetTraceResult().Threads[0].Methods[0].ClassName);
        }

        [TestMethod]
        public void resultThreadId()
        {
            Tracer tracer = new Tracer();

            tracer.StartTrace();
            tracer.StopTrace();

            Assert.AreEqual(Thread.CurrentThread.ManagedThreadId, tracer.GetTraceResult().Threads[0].Id);
        }

        [TestMethod]
        public void resultMethodTime()
        {
            Tracer tracer = new Tracer();

            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();

            Assert.AreEqual(100, tracer.GetTraceResult().Threads[0].Methods[0].EllapsedTime, 20);
        }

        [TestMethod]
        public void resultThreadTime()
        {
            Tracer tracer = new Tracer();

            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();

            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();

            Assert.AreEqual(200, tracer.GetTraceResult().Threads[0].EllapsedTime, 20);
        }

        void firstMethod(ITracer tracer)
        {
            tracer.StartTrace();
            secondMethod(tracer);
            tracer.StartTrace();
        }

        void secondMethod(ITracer tracer)
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        [TestMethod]
        public void resultNestedMethodLevel2()
        {
            Tracer tracer = new Tracer();

            firstMethod(tracer);

            Assert.IsNotNull(tracer.GetTraceResult().Threads[0].Methods[0].Methods[0]);
        }

        void methodInAnotherThread()
        {
            sharedTracer.StartTrace();
            Thread.Sleep(1000);
            sharedTracer.StopTrace();
            isResultReady = true;
        }

        [TestMethod]
        public void resultWhen2Threads()
        {
            sharedTracer = new Tracer();

            isResultReady = false;
            Thread thread = new Thread(methodInAnotherThread);
            thread.Start();

            sharedTracer.StartTrace();
            sharedTracer.StopTrace();

            while (!isResultReady) ;
            Assert.IsNotNull(sharedTracer.GetTraceResult().Threads[1]);
        }
    }
}
