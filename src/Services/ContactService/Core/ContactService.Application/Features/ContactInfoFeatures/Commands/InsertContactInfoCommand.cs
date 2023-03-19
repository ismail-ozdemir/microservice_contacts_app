using AutoMapper;
using ContactService.Application.Dto.ContactInfo;
using ContactService.Application.Exceptions;
using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Entities;
using MediatR;

namespace ContactService.Application.Features.ContactInfoFeatures
{



    public class InsertContactInfoCommand : IRequest<SaveContactInfoResponseDto>
    {
        internal InsertContactInfoRequest @params;
        public InsertContactInfoCommand(InsertContactInfoRequest newContactInfo)
        {
            @params = newContactInfo ?? throw new ArgumentNullException(nameof(newContactInfo));
        }
        internal class InsertContactInfoCommandHandler : IRequestHandler<InsertContactInfoCommand, SaveContactInfoResponseDto>
        {

            private readonly IMapper _mapper;
            private readonly IContactInfoRepository _contRepo;
            private readonly IPersonRepository _personRepo;
            public InsertContactInfoCommandHandler(IMapper mapper, IContactInfoRepository contactInfoRepository, IPersonRepository personRepository)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _personRepo = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
                _contRepo = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
            }

            public async Task<SaveContactInfoResponseDto> Handle(InsertContactInfoCommand request, CancellationToken cancellationToken)
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request));

                bool hasPerson = await _personRepo.CheckedPersonByIdAsync(request.@params.PersonId);
                if (!hasPerson)
                    throw new RecordNotFoundException($"{request.@params.PersonId} Id değerine sahip kullanıcı bulunamadı");

                var contactInfo = _mapper.Map<ContactInformation>(request.@params);
                var newContactInfo = await _contRepo.AddAsync(contactInfo);
                var result = _mapper.Map<SaveContactInfoResponseDto>(newContactInfo);
                return result;

            }
        }

    }
}
