using AutoMapper;
using ContactService.Application.Dto.Person;
using ContactService.Application.Mapping;
using ContactService.Core.Domain.Entities;

namespace ContactService.UnitTest.Application.Mappings
{
    public class PersonMapping_UnitTest
    {

        private MapperConfiguration config;
        private IMapper mapper;
        [SetUp]
        public void Setup()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<PersonMapping>());
            mapper = config.CreateMapper();
        }


        [Test]
        public void PersonMapping_Configuration_IsValid()
        {
            config.AssertConfigurationIsValid();
        }

    }
}
