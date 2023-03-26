
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    public class CommonBaseException : Exception
    {
        public CommonBaseException() : base()
        {

        }
        public CommonBaseException(string message) : base(message)
        {

        }
        public CommonBaseException(string message, Exception innerException) : base(message, innerException)
        {

        }
        public long ServiceInvokeDurationInTicks { get; set; }
        public long ServiceInvokeDurationInMilliseconds { get; set; }
    }
}
