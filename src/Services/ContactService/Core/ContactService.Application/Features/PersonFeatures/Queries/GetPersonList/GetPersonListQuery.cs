using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
using MediatR;

namespace ContactService.Application.Features.PersonFeatures.Queries
{


    public class GetPersonListQuery : IRequest<PagedResult<PersonDto>>
    {
        public PersonFilter Filter { get; set; }

        public GetPersonListQuery(PersonFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }
    }
}
