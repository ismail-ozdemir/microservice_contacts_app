
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class RecordNotFoundException : CommonBaseException
    {
        public RecordNotFoundException(string message) : base(message)
        {

        }

        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
