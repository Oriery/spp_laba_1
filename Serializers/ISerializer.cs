using Tracer;

namespace Serializers
{
    public interface ISerializer
    {
        string FileFormat { get; }

        string Serialize(TraceResult traceResult);
    }
}