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
                throw new ArgumentNullException("person", "record not found exception yazılmalı");

            await _personRepository.RemoveAsync(person);

            return $"{request.Id} deleted succesful";
        }
    }
}
