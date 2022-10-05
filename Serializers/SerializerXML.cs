using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using System.Xml;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Serializers
{
    public class SerializerXML : ISerializer
    {
        public string FileFormat => "xml";

        public string Serialize(TraceResult traceResult)
        {
            using var memoryStream = new MemoryStream();
            var ser = new DataContractSerializer(typeof(TraceResult));

            using (XmlWriter xw = XmlWriter.Create(memoryStream, new()
            {
                Encoding = Encoding.UTF8,
                Indent = true
            }))
            {
                ser.WriteObject(xw, traceResult);
            }
            return Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
