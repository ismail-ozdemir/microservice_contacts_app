﻿using AutoMapper;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Mapping;
using ContactService.Domain.Entities;

namespace ContactService.Application.UnitTest.Mappings
{
    public class PersonMapping_UnitTest
    {

        private MapperConfiguration config;
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            config = new MapperConfiguration(cfg =>
            {
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
            var command = new CreatePersonCommand { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var person = _mapper.Map<Person>(command);
            Assert.IsTrue(command.Name == person.Name && command.Surname == person.Surname && command.Company == person.Company);

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
