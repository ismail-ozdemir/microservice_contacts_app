using AutoMapper;
using Common.Shared.Exceptions;
using ContactService.Application.Interfaces.Repository;
using MediatR;
using Common.Shared.Wrappers;
using ContactService.Shared.Filters;
using ContactService.Shared.Dto.PersonDtos;
using ContactService.Shared.Dto.ContactInfoDtos;

namespace ContactService.Application.Features.PersonFeatures.Queries
{




    public class GetPersonContactInfoList : IRequest<PersonResponse.WithContactInfo>
    {
        public PersonFilter.ById Filter { get; set; }
        public GetPersonContactInfoList(PersonFilter.ById filter)
        {
            Filter = filter ?? throw new ArgumentNullException(nameof(filter));
        }


        internal class GetPersonContactInfoListHandler : IRequestHandler<GetPersonContactInfoList, PersonResponse.WithContactInfo>
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

            public async Task<PersonResponse.WithContactInfo> Handle(GetPersonContactInfoList request, CancellationToken cancellationToken)
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                var person = await _personRepository.GetByIdAsync(request.Filter.Id);
                if (person == null)
                    throw new RecordNotFoundException(nameof(person));

                var contactInfoList = await _contactInfoRepository.GetContactInfoListByPersonAsync(request.Filter)!;
                var infoList = _mapper.Map<PagedResult<ContactInfoResponseDto>>(contactInfoList);
                var result = new PersonResponse.WithContactInfo()
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
