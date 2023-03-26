
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class FileNotFoundException : CommonExceptionBase
    {
        public FileNotFoundException(string message) : base(message)
        {
        }

        public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
