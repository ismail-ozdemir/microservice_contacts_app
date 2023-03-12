using AutoMapper;
using ContactService.Application.Dto.Person;
using ContactService.Application.Mapping;
using ContactService.Core.Domain.Entities;

namespace ContactService.UnitTest.Application.Mappings
{
    public class PersonelMapping_UnitTest
    {

        private MapperConfiguration config;
        private IMapper mapper;
        [SetUp]
        public void Setup()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<PersonelMapping>());
            mapper = config.CreateMapper();
        }


        [Test]
        public void PersonelMapping_Configuration_IsValid()
        {
            config.AssertConfigurationIsValid();
        }


        [Test]
        public void PersonelMapping_ConvertCreatePersonRequestToPerson_IsValid()
        {
            var req = new CreatePersonRequest { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var person = mapper.Map<Person>(req);

            Assert.IsTrue(req.Name == person.Name && req.Surname == person.Surname && req.Company == person.Company);
        }
    }
}
