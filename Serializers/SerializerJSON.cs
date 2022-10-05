using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tracer;

namespace Serializers
{
    public class SerializerJSON : ISerializer
    {
        public string FileFormat => "json";

        public string Serialize(TraceResult traceResult)
        {
            var opt = new JsonSerializerOptions() { WriteIndented = true };

            var byteArray = JsonSerializer.SerializeToUtf8Bytes(traceResult, opt);

            return Encoding.UTF8.GetString(byteArray);
        }
    }
}
