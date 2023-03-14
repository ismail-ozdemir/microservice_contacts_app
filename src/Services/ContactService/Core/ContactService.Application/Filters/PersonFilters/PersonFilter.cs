using ContactService.Application.Parameters;

namespace ContactService.Application.Filters.PersonFilters
{
    public class PersonFilter : PagedFilter
    {

        public class ByCompany : PersonFilter
        {
            public string Company { get; set; }
        }
    }
}
