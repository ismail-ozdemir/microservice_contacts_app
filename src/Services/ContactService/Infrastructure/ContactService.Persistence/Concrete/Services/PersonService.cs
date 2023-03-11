using ContactService.Application.Dto.Person;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Interfaces.Services;
using ContactService.Application.Wrappers;
using ContactService.Core.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace ContactService.Persistence.Concrete.Services
{
    internal class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonService> _logger;

        public PersonService(IPersonRepository personRepository, ILogger<PersonService> logger)
        {
            _personRepository = personRepository;
            _logger = logger;
        }

        public async Task<ServiceResponse<CreatePersonResponse>> AddAsync(CreatePersonRequest request)
        {

            var person = new Person
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Surname = request.Surname,
                Company=request.Company,
            };

            _logger.LogInformation("Person saving...");
            await _personRepository.AddAsync(person);
            await _personRepository.SaveAsync();
            _logger.LogInformation("Person saved.");
            var response = new CreatePersonResponse
            {
                PersonId = person.Id,
                Name = person.Name,
                Surname = person.Surname,
                Company = person.Company

            };
            return new ServiceResponse<CreatePersonResponse>(response);

        }
    }
}
