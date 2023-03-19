namespace ContactService.Application.Helpers.Pagination
{
    public abstract class PagedResultBase
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalRecordCount { get; set; }

    }


}
