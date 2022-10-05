namespace spp_laba_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();

            TestClass testClass = new(tracer);
            testClass.Method1();
            testClass.Method1();

            TraceResult traceResult = tracer.GetTraceResult();

            Console.WriteLine(traceResult.ToString());
        }
    }

    public class TestClass
    {
        private Tracer _tracer;

        public TestClass(Tracer tracer)
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