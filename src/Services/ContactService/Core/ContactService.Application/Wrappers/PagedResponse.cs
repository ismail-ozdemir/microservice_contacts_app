namespace ContactService.Application.Wrappers
{
    public class PagedResponse<T> : ServiceResponse<T>
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public PagedResponse(T value, int pageNo = 1, int pageSize = 10) : base(value)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
