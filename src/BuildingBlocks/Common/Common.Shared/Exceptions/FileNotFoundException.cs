
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
    public class FileNotFoundException : CommonBaseException
    {
        public FileNotFoundException(string message) : base(message)
        {
        }

        public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }
    }
}
