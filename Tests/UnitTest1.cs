using Tracer;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestBasics()
        {
            Tracer.Tracer tracer = new Tracer.Tracer();

            TestClass testClass = new(tracer);
            testClass.Method1();
            testClass.Method1();

            TraceResult traceResult = tracer.GetTraceResult();

            Assert.IsTrue(
                traceResult.ThreadTraceResults[0].Time > 1600 &&
                traceResult.ThreadTraceResults[0].Time < 1800
                );

            Assert.IsTrue(
                traceResult.ThreadTraceResults[0].MethodTraceResults[0].Time > 800 &&
                traceResult.ThreadTraceResults[0].MethodTraceResults[0].Time < 900
                );

            Assert.AreEqual(
                traceResult.ThreadTraceResults[0].MethodTraceResults[0].Name,
                "Method1"
                );

            Assert.AreEqual(
                traceResult.ThreadTraceResults[0].MethodTraceResults[0].ClassName,
                "Tests.TestClass"
                );
        }

        [TestMethod]
        public void TestMultithreading()
        {
            Tracer.Tracer tracer = new Tracer.Tracer();

            TestClass testClass = new(tracer);
            testClass.Method1();
            testClass.Method1();

            var t1 = new Thread(() =>
            {
                testClass.Method1();
            });
            t1.Start(); 

            var t2 = new Thread(() =>
            {
                testClass.Method1();
                testClass.Method1();
            });
            t2.Start();

            testClass.MethodWithMultithreadingInside();

            t1.Join();
            t2.Join();

            TraceResult traceResult = tracer.GetTraceResult();

            Assert.AreEqual(traceResult.ThreadTraceResults.Count, 4);
        }
    }

    public class TestClass
    {
        private Tracer.Tracer _tracer;

        public TestClass(Tracer.Tracer tracer)
        {
            this._tracer = tracer;
        }

        public void Method1()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            Method2();
            Method2();
            Method3();
            _tracer.StopTrace();
        }

        private void Method2()
        {
            _tracer.StartTrace();
            Thread.Sleep(200);
            _tracer.StopTrace();
        }

        private void Method3()
        {
            _tracer.StartTrace();
            Thread.Sleep(300);
            _tracer.StopTrace();
        }

        public void MethodWithMultithreadingInside()
        {
            _tracer.StartTrace();
            var t2 = new Thread(() =>
            {
                Method2();
            });
            t2.Start();

            Method2();

            t2.Join();

            _tracer.StopTrace();
        }
    }
}