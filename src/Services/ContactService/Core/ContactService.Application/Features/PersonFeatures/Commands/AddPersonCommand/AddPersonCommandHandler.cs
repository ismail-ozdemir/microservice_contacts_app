﻿using AutoMapper;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Entities;
using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class AddPersonCommandHandler : IRequestHandler<AddPersonCommand, CreatePersonResponse>
    {

        private readonly IMapper _mapper;
        private readonly IPersonRepository _personRepository;

        public AddPersonCommandHandler(IMapper mapper, IPersonRepository personRepository)
        {
            _mapper = mapper;
            _personRepository = personRepository;
        }

        public async Task<CreatePersonResponse> Handle(AddPersonCommand request, CancellationToken cancellationToken)
        {

            Person person = _mapper.Map<Person>(request)!;
            var newPerson = await _personRepository.AddAsync(person);
            var result = _mapper.Map<CreatePersonResponse>(newPerson);
            return result;

        }
    }


}
