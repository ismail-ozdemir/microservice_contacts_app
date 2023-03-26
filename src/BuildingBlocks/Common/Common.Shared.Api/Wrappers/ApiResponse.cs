using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Api.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class ApiResponse
    {
        public bool IsCompletedSuccessfully { get; set; }
        public long ServiceInvokeDurationInTicks { get; set; }
        public long ServiceInvokeDurationInMilliseconds { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class ApiResponse<TData> : ApiResponse
    {
        public TData? Data { get; set; }
    }
}
