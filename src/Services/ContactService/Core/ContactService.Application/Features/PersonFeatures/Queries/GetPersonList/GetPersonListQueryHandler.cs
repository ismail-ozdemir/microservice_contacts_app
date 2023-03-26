using AutoMapper;
using Common.Shared.Wrappers;
using ContactService.Application.Interfaces.Repository;
using ContactService.Shared.Dto.PersonDtos;
using MediatR;

namespace ContactService.Application.Features.PersonFeatures.Queries
{
    public class GetPersonListQueryHandler : IRequestHandler<GetPersonListQuery, PagedResult<PersonResponse>>
    {

        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;
        public GetPersonListQueryHandler(IPersonRepository personRepository, IMapper mapper)
        {
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PagedResult<PersonResponse>> Handle(GetPersonListQuery request, CancellationToken cancellationToken)
        {

            var personList = await _personRepository.GetPersonsAsync(request.Filter, cancellationToken);
            return _mapper.Map<PagedResult<PersonResponse>>(personList);
        }

    }
}
