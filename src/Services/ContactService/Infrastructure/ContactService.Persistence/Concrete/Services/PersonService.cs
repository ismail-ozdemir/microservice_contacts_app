using AutoMapper;
using ContactService.Application.Dto.Person;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Interfaces.Services;
using ContactService.Application.Wrappers;
using ContactService.Core.Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace ContactService.Persistence.Concrete.Services
{
    internal class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly ILogger<PersonService> _logger;
        private readonly IValidator<CreatePersonRequest> _createPersonValidator;
        private readonly IMapper _mapper;

        public PersonService(IPersonRepository personRepository, ILogger<PersonService> logger, IValidator<CreatePersonRequest> createPersonValidator, IMapper mapper)
        {
            _personRepository = personRepository;
            _logger = logger;
            _createPersonValidator = createPersonValidator;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CreatePersonResponse>> AddAsync(CreatePersonRequest request)
        {
            _logger.LogInformation("Person validating...");
            await _createPersonValidator.ValidateAndThrowAsync(request);
            _logger.LogInformation("Person validated...");

            _logger.LogInformation("Person is mapping...");
            var person = _mapper.Map<Person>(request);
            _logger.LogInformation("Person was mapped...");

            _logger.LogInformation("Person is adding...");
            await _personRepository.AddAsync(person);
            _logger.LogInformation("Person added...");

            await _personRepository.SaveAsync();
            _logger.LogInformation("Person saved.");


            _logger.LogInformation("Person response is mapping...");
            var response = _mapper.Map<CreatePersonResponse>(person);
            _logger.LogInformation("Person response was mapped.");

            return new ServiceResponse<CreatePersonResponse>(response);

        }
    }
}
