using AutoMapper;

using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Entities;
using ContactService.Shared.Dto.PersonDtos;
using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, CreatePersonResponseDto>
    {

        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public CreatePersonCommandHandler(IMapper mapper, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<CreatePersonResponseDto> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {

            Person person = _mapper.Map<Person>(command.request);
            var newPerson = await _personRepository.AddAsync(person);
            var result = _mapper.Map<CreatePersonResponseDto>(newPerson);
            return result;

        }
    }


}
