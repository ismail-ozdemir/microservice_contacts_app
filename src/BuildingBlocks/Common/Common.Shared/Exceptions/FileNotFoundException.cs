
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class FileNotFoundException : CommonBaseException
    {
        public FileNotFoundException(string message) : base(message)
        {
        }

        public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
