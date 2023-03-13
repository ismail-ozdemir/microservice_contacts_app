using AutoMapper;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Mapping;
using ContactService.Domain.Entities;
using ContactService.Persistence.Concrete.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework.Internal;

namespace ContactService.Persistence.UnitTest.Concrete.Services
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
        public async Task PersonService_AddAsyncValid_ServiceResponse()
        {
            var reqData = new CreatePersonRequest() { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var mock = new Mock<IValidator<CreatePersonRequest>>();
            var personService = new PersonService(_personRepository, _logger, mock.Object, _mapper);
            var response = await personService.AddAsync(reqData);
            Assert.IsTrue(response.isSuccess && response.Data.PersonId != Guid.Empty);

        }

        [Test]
        public void PersonService_UpdateAsync_ServiceResponse()
        {

            Assert.Fail("Test Yazılmadı...");
        }
        [Test]
        public void PersonService_DeleteAsync_ServiceResponse()
        {

            Assert.Fail("Test Yazılmadı...");
        }
        [Test]
        public void PersonService_GetPersonListWithPage_PagedResponse()
        {

            Assert.Fail("Test Yazılmadı...");
        }
















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
                        return Task.FromResult(person);
                    });


            return mock.Object;
        }

    }
}
