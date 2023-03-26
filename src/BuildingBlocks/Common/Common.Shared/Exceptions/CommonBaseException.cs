
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Common.Shared.Exceptions
{
    [ExcludeFromCodeCoverage]
    [Serializable]
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
        protected CommonBaseException(SerializationInfo info, StreamingContext context): base(info, context)
        {
            this.ServiceInvokeDurationInTicks = info.GetDouble(nameof(CommonBaseException.ServiceInvokeDurationInTicks));
            this.ServiceInvokeDurationInMilliseconds = info.GetDouble(nameof(CommonBaseException.ServiceInvokeDurationInMilliseconds));
        }
        public double ServiceInvokeDurationInTicks { get; set; }
        public double ServiceInvokeDurationInMilliseconds { get; set; }
    }
}
