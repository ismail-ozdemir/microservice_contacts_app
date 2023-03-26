
namespace Common.Shared.Exceptions
{
    public class CommonExceptionBase : Exception
    {
        public CommonExceptionBase() : base()
        {

        }
        public CommonExceptionBase(string message) : base(message)
        {

        }
        public CommonExceptionBase(string message, Exception innerException) : base(message, innerException)
        {

        }
        public long ServiceInvokeDurationInTicks { get; set; }
        public long ServiceInvokeDurationInMilliseconds { get; set; }
    }
}
