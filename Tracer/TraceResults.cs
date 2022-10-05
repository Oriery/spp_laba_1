using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{

    [DataContract]
    public struct TraceResult
    {
        [DataMember]
        private List<ThreadTraceResult> _threadTraceResults;

        public IReadOnlyList<ThreadTraceResult> ThreadTraceResults { get => _threadTraceResults; }


        public TraceResult()
        {
            _threadTraceResults = new();
        }

        public void AddThread(ThreadTraceResult threadTraceResult)
        {
            _threadTraceResults.Add(threadTraceResult);
        }
    }

    [DataContract]
    public struct ThreadTraceResult
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        private long _time
        {
            get
            {
                long res = 0;
                foreach (var methodResult in MethodTraceResults)
                {
                    res = res + methodResult.Time;
                }
                return res;
            }

            set {}
        }

        public long Time { get => _time; }

            [DataMember]
        private List<MethodTraceResult> _methodTraceResults;

        public IReadOnlyList<MethodTraceResult> MethodTraceResults { get => _methodTraceResults; }

        public ThreadTraceResult(int Id)
        {
            _methodTraceResults = new();
            this.Id = Id;
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            _methodTraceResults.Add(methodTraceResult);
        }
    }

    [DataContract]
    public struct MethodTraceResult
    {
        public Stopwatch stopwatch;

        [DataMember]
        private long _time
        {
            get
            {
                return stopwatch.ElapsedMilliseconds;
            }

            set { }
        }

        public long Time { get => _time; }

        [DataMember]
        private string _name;
        public string Name { get => _name; }

        [DataMember]
        private string _className;
        public string ClassName { get => _className; }

        [DataMember]
        private List<MethodTraceResult> _methodTraceResults;

        public IReadOnlyList<MethodTraceResult> MethodTraceResults { get => _methodTraceResults; }

        public MethodTraceResult(string ClassName, string Name)
        {
            _methodTraceResults = new();
            stopwatch = new();
            _name = Name;
            _className = ClassName;
        }

        public void AddMethod(MethodTraceResult methodTraceResult)
        {
            _methodTraceResults.Add(methodTraceResult);
        }
    }
}
