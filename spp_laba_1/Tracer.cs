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
        private ConcurrentDictionary<int, ConcurrentStack<MethodTraceResult>> StacksForMethodsOfThreads;
        public ConcurrentDictionary<int, ThreadTraceResult> Threads;

        public Tracer()
        {
            StacksForMethodsOfThreads = new();
        }

        void ITracer.StartTrace()
        {
            int ThreadId = Thread.CurrentThread.ManagedThreadId;
            var frame = new StackTrace(true).GetFrame(1);
            var ClassName = frame?.GetMethod()?.DeclaringType?.FullName;
            var MethodName = frame?.GetMethod()?.Name;
            var stack = StacksForMethodsOfThreads.GetOrAdd(ThreadId, new ConcurrentStack<MethodTraceResult>());
            var MethodResult = new MethodTraceResult(ClassName, MethodName);
            stack.Push(MethodResult);
            
            MethodResult.stopwatch.Start();
        }

        void ITracer.StopTrace()
        {
            var ThreadId = Thread.CurrentThread.ManagedThreadId;
            ConcurrentStack<MethodTraceResult> stack = StacksForMethodsOfThreads.GetOrAdd(ThreadId, new ConcurrentStack<MethodTraceResult>());
            stack.TryPop(out var method);
            method.stopwatch.Stop();
            if (stack.TryPeek(out var parent))
            {
                parent.MethodTraceResults.Add(method);
            }
            else
            {
                var Thread = Threads.GetOrAdd(ThreadId, new ThreadTraceResult(ThreadId));
                Thread.MethodTraceResults.Add(method);
            }
        }

        TraceResult ITracer.GetTraceResult()
        {
            TraceResult traceResult = new();
            foreach (var Thread in Threads.Values)
            {
                traceResult.ThreadTraceResults.Add(Thread);
            }

            return traceResult;
        }
    }
}
