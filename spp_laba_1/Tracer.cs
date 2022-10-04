using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp_laba_1
{
    public class Tracer : ITracer
    {
        private Stopwatch stopwatch;

        public Tracer()
        {
            stopwatch = new();
        }

        void ITracer.StartTrace()
        {
            throw new NotImplementedException();
        }

        void ITracer.StopTrace()
        {
            throw new NotImplementedException();
        }

        TraceResult ITracer.GetTraceResult()
        {
            throw new NotImplementedException();
        }
    }
}
