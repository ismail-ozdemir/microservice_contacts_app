namespace ContactService.Application.Wrappers
{
    public class ServiceResponse<T>
    {
        public Guid TraceId { get; set; }
        public virtual bool isSuccess { get; set; } = true;
        public T Data { get; set; }

        public ServiceResponse(T value)
        {
            Data = value;
        }
    }
}
