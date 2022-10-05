using Serializers;
using Tracer;

namespace spp_laba_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Tracer.Tracer tracer = new Tracer.Tracer();

            TestClass testClass = new(tracer);
            testClass.Method1();
            testClass.Method1();
            var t = new Thread(() =>
            {
                testClass.Method1();
            });
            t.Start();
            t.Join();

            TraceResult traceResult = tracer.GetTraceResult();

            ISerializer serializer = new SerializerJSON();
            string str = serializer.Serialize(traceResult);

            Console.WriteLine(str);
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