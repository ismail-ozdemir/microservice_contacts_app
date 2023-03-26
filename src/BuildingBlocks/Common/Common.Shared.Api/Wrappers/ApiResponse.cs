namespace Common.Shared.Api.Wrappers
{
    public class ApiResponse
    {
        public bool IsCompletedSuccessfully { get; set; }
        public long ServiceInvokeDurationInTicks { get; set; }
        public long ServiceInvokeDurationInMilliseconds { get; set; }
    }
    public class ApiResponse<TData> : ApiResponse
    {
        public TData? Data { get; set; }
    }
}
