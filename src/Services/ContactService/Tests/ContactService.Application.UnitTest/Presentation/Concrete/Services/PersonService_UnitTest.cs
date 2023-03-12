using AutoMapper;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Mapping;
using ContactService.Application.Validators.Person;
using ContactService.Core.Domain.Entities;
using ContactService.Persistence.Concrete.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;

namespace ContactService.UnitTest.Presentation.Concrete.Services
{
    // TO DO : beklenen exception tipleri için test yazılması gerekli.
    public class PersonService_UnitTest
    {

        IMapper _mapper;
        IPersonRepository _personRepository;
        ILogger<PersonService> _logger;

        [SetUp]
        public void Setup()
        {
            _mapper = GetMapper();
            _personRepository = GetPersonRepository();
            _logger = GetLogger();


        }

        [Test]
        public async Task PersonService_AddAsync_ServiceResponse()
        {
            var validator = new CreatePersonValidator();
            var personService = new PersonService(_personRepository, _logger, validator, _mapper);
            var response = await personService.AddAsync(new() { Name = "ismail", Surname = "Özdemir", Company = "github" });

            Assert.IsTrue(response.isSuccess && response.Data.PersonId != Guid.Empty);

        }
        [Test]
        public void PersonService_NotValidAddAsync_ValidationException()
        {
            var validator = new CreatePersonValidator();
            var personService = new PersonService(_personRepository, _logger, validator, _mapper);
            var ex = Assert.Catch(() => { personService.AddAsync(new() { Name = "", Surname = "Özdemir", Company = "github" }).Wait(); });
            Assert.IsTrue(ex.InnerException is FluentValidation.ValidationException);
        }

        //[Test]
        //public void PersonService_UpdateAsync_ServiceResponse()
        //{

        //}
        //[Test]
        //public void PersonService_DeleteAsync_ServiceResponse()
        //{

        //}
        //[Test]
        //public void PersonService_GetPersonListWithPage_PagedResponse()
        //{

        //}
















        public IMapper GetMapper()
        {
            var mappingProfile = new PersonMapping();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(mappingProfile));
            return new Mapper(configuration);
        }

        private ILogger<PersonService> GetLogger()
        {
            var serviceProvider = new ServiceCollection().AddLogging().BuildServiceProvider();

            var factory = serviceProvider.GetService<ILoggerFactory>();

            return factory.CreateLogger<PersonService>();
        }

        private IPersonRepository GetPersonRepository()
        {
            var mock = new Mock<IPersonRepository>();
            mock.Setup(m => m.AddAsync(It.IsAny<Person>()))
                .Returns<Person>(
                    (person) =>
                    {
                        person.Id = Guid.NewGuid();
                        return Task.FromResult(true);
                    });


            return mock.Object;
        }

    }
}
