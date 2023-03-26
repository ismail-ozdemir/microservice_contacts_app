
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class RecordNotFoundException : CommonExceptionBase
    {
        public RecordNotFoundException(string message) : base(message)
        {

        }

        public RecordNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
