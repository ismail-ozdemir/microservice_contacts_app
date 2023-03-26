﻿using Common.Shared.Exceptions;
using ContactService.Application.Interfaces.Repository;
using MediatR;

namespace ContactService.Application.Features.PersonFeatures.Commands
{
    internal class RemovePersonCommandHandler : IRequestHandler<RemovePersonCommand, string>
    {

        private readonly IPersonRepository _personRepository;
        public RemovePersonCommandHandler(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<string> Handle(RemovePersonCommand request, CancellationToken cancellationToken)
        {

            var person = await _personRepository.GetByIdAsync(request.Id);
            if (person == null)
                throw new RecordNotFoundException(nameof(person));

            await _personRepository.RemoveAsync(person);

            return $"{request.Id} deleted succesfull";
        }
    }
}
