
using System.Diagnostics.CodeAnalysis;

namespace Common.Shared.Filters
{
    [ExcludeFromCodeCoverage]
    public class PagedFilter
    {
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
