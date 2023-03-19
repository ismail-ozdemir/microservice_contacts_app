using AutoMapper;
using ContactService.Application.Dto.ContactInfo;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Exceptions;
using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
using ContactService.Application.Interfaces.Repository;
using MediatR;

namespace ContactService.Application.Features.PersonFeatures.Queries
{




    public class GetPersonContactInfoList : IRequest<PersonDto.WithContactInfo>
    {
        public PersonFilter.ById Filter { get; set; }
        public GetPersonContactInfoList(PersonFilter.ById filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }


        internal class GetPersonContactInfoListHandler : IRequestHandler<GetPersonContactInfoList, PersonDto.WithContactInfo>
        {

            private readonly IMapper _mapper;
            private readonly IPersonRepository _personRepository;
            private readonly IContactInfoRepository _contactInfoRepository;

            public GetPersonContactInfoListHandler(IMapper mapper, IPersonRepository personRepository, IContactInfoRepository contactInfoRepository)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
                _contactInfoRepository = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
            }

            public async Task<PersonDto.WithContactInfo> Handle(GetPersonContactInfoList request, CancellationToken cancellationToken)
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                var person = await _personRepository.GetByIdAsync(request.Filter.Id);
                if (person == null)
                    throw new RecordNotFoundException(nameof(person));

                var contactInfoList = await _contactInfoRepository.GetContactInfoListByPersonAsync(request.Filter)!;
                var infoList = _mapper.Map<PagedResult<ContactInfoDto>>(contactInfoList);
                var result = new PersonDto.WithContactInfo()
                {
                    Id = person.Id,
                    Name = person.Name,
                    Company = person.Company,
                    Surname = person.Surname,
                    ContactInformations = infoList
                };

                return result;

            }
        }

    }
}
