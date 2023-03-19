using ContactService.Application.Parameters;

namespace ContactService.Application.Filters.PersonFilters
{
    public class PersonFilter : PagedFilter
    {

        public class ById : PersonFilter
        {
            public Guid Id { get; set; }
        }
    }
}
