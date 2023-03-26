using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Api.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class ApiResponse
    {
        public bool IsCompletedSuccessfully { get; set; }
        public double ServiceInvokeDurationInTicks { get; set; }
        public double ServiceInvokeDurationInMilliseconds { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class ApiResponse<TData> : ApiResponse
    {
        public TData? Data { get; set; }
    }
}
