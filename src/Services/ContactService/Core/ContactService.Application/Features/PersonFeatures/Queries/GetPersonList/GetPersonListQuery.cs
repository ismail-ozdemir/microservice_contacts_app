using Common.Shared.Wrappers;
using ContactService.Shared.Dto.PersonDtos;
using ContactService.Shared.Filters;
using MediatR;

namespace ContactService.Application.Features.PersonFeatures.Queries
{

    public class GetPersonListQuery : IRequest<PagedResult<PersonResponse>>
    {
        public PersonFilter Filter { get; set; }

        public GetPersonListQuery(PersonFilter filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }
    }
}
