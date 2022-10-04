﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spp_laba_1
{
    public struct TraceResult
    {
        public List<ThreadTraceResult> ThreadTraceResults;

        public TraceResult()
        {
            ThreadTraceResults = new();
        }
    }

    public struct ThreadTraceResult
    {
        public int Id { get; set; }
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
        }
        public List<MethodTraceResult> MethodTraceResults { get; set; }

        public ThreadTraceResult(int Id)
        {
            MethodTraceResults = new();
            this.Id = Id;
        }
    }

    public struct MethodTraceResult
    {
        public long Time { get; set; }
        public string Name { get; }
        public string ClassName { get; }

        public List<MethodTraceResult> MethodTraceResults { get; set; }

        public MethodTraceResult(string ClassName, string Name)
        {
            MethodTraceResults = new List<MethodTraceResult>();
            Time = 0;
            this.Name = Name;
            this.ClassName = ClassName;
        }
    }
}
