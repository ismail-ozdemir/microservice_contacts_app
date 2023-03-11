namespace ContactService.Application.Wrappers
{
    public abstract class ServiceResponse<T>
    {
        public T Value { get; set; }

        protected ServiceResponse(T value)
        {
            Value = value;
        }
    }
}
