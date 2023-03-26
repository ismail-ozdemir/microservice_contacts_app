
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Wrappers
{
    [ExcludeFromCodeCoverage]
    public class PagedResult<T> where T : class
    {
        public int PageNo { get; set; }
        public int PageSize { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalRecordCount { get; set; }
        public IList<T> Results { get; set; }
        public PagedResult() => Results = new List<T>();
    }
}
