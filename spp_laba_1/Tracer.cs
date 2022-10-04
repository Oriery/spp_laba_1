using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp_laba_1
{
    public class Tracer : ITracer
    {
        private ConcurrentDictionary<int, ConcurrentStack<MethodTraceResult>> threads;
        public ConcurrentDictionary<int, List<MethodTraceResult>> DoneMethods;
        // TODO Лучше наверное хранить ThreadTraceResult

        public Tracer()
        {
            threads = new();
        }

        void ITracer.StartTrace()
        {
            int ThreadId = Thread.CurrentThread.ManagedThreadId;
            var frame = new StackTrace(true).GetFrame(1);
            var ClassName = frame?.GetMethod()?.DeclaringType?.FullName;
            var MethodName = frame?.GetMethod()?.Name;
            var stack = threads.GetOrAdd(ThreadId, new ConcurrentStack<MethodTraceResult>());
            var MethodResult = new MethodTraceResult(ClassName, MethodName);
            stack.Push(MethodResult);
            
            MethodResult.stopwatch.Start();
        }

        void ITracer.StopTrace()
        {
            var ThreadId = Thread.CurrentThread.ManagedThreadId;
            ConcurrentStack<MethodTraceResult> stack = threads.GetOrAdd(ThreadId, new ConcurrentStack<MethodTraceResult>());
            stack.TryPop(out var method);
            method.stopwatch.Stop();
            if (stack.TryPeek(out var parent))
            {
                parent.MethodTraceResults.Add(method);
            }
            else
            {
                var List = DoneMethods.GetOrAdd(ThreadId, new List<MethodTraceResult>());
                List.Add(method);
            }
        }

        TraceResult ITracer.GetTraceResult()
        {
            throw new NotImplementedException();
        }
    }
}
