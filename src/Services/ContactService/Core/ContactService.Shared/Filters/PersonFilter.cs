using Common.Shared.Filters;
using System.Diagnostics.CodeAnalysis;

namespace ContactService.Shared.Filters
{
    [ExcludeFromCodeCoverage]
    public class PersonFilter : PagedFilter
    {
        public class ById : PersonFilter
        {
            public Guid Id { get; set; }
        }
    }
}
