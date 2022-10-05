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
        private List<ThreadTraceResult> _threadTraceResults;

        [DataMember]
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
        public long Time
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

        [DataMember]
        public List<MethodTraceResult> MethodTraceResults { get; set; }

        public ThreadTraceResult(int Id)
        {
            MethodTraceResults = new();
            this.Id = Id;
        }
    }

    [DataContract]
    public struct MethodTraceResult
    {
        public Stopwatch stopwatch;

        [DataMember]
        public long Time
        {
            get
            {
                return stopwatch.ElapsedMilliseconds;
            }

            set { }
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ClassName { get; set; }

        [DataMember]
        public List<MethodTraceResult> MethodTraceResults { get; set; }

        public MethodTraceResult(string ClassName, string Name)
        {
            MethodTraceResults = new();
            stopwatch = new();
            this.Name = Name;
            this.ClassName = ClassName;
        }
    }
}
