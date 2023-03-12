using AutoMapper;
using ContactService.Application.Dto.Person;
using ContactService.Application.Mapping;
using ContactService.Core.Domain.Entities;

namespace ContactService.UnitTest.Application.Mappings
{
  
    public class PersonMapping_UnitTest
    {

        private MapperConfiguration config;
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            config = new MapperConfiguration(cfg => {
                cfg.AddProfile<PersonMapping>();
            });
            

            _mapper = config.CreateMapper();
            
        }


        [Test]
        public void PersonMapping_Configuration_IsValid()
        {
            config.AssertConfigurationIsValid();
        }


        [Test]
        public void PersonMapping_ConvertCreatePersonRequestToPerson_IsValid()
        {
            var req = new CreatePersonRequest { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var person = _mapper.Map<Person>(req);
            Assert.IsTrue(req.Name == person.Name && req.Surname == person.Surname && req.Company == person.Company);

        }

        [Test]
        public void PersonMapping_ConvertPersonToCreatePersonResponse_IsValid()
        {
            var req = new Person { Id = Guid.NewGuid(), Name = "ismail", Surname = "Özdemir", Company = "github" };
            var res = _mapper.Map<CreatePersonResponse>(req);
            Assert.IsTrue(
                req.Id == res.PersonId &&
                req.Name == res.Name &&
                req.Surname == res.Surname &&
                req.Company == res.Company);

        }

    }
}
