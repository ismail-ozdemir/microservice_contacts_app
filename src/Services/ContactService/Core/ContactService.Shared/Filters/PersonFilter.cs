using Common.Shared.Filters;

namespace ContactService.Shared.Filters
{
    public class PersonFilter : PagedFilter
    {
        public class ById : PersonFilter
        {
            public Guid Id { get; set; }
        }
    }
}
