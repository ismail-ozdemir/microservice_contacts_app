namespace ContactService.Application.Parameters
{
    public class PagedDataRequest
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }

        public PagedDataRequest(int pageNo, int pageSize)
        {
            PageNo = pageNo;
            PageSize = pageSize;
        }
    }
}
