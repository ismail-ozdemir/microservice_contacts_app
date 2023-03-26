
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class RecordNotFoundException : CommonBaseException
    {
        public RecordNotFoundException(string message) : base(message)
        {

        }

        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected RecordNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
