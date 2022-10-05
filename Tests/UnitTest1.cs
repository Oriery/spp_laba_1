using Tracer;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
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
    }
}