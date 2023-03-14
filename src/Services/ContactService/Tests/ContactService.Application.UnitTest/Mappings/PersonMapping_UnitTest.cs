using AutoMapper;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Helpers.Pagination;
using ContactService.Application.Mapping;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Domain.Entities;

namespace Mappings
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
            var res = _mapper.Map<CreatePersonResponseDto>(req);
            Assert.IsTrue(
                req.Id == res.PersonId &&
                req.Name == res.Name &&
                req.Surname == res.Surname &&
                req.Company == res.Company);

        }



        [Test]
        public void PersonMapping_ConvertPersonWmToPersonDto_IsValid()
        {
            var req = new PersonWm { Id = Guid.NewGuid(), Name = "ismail", Surname = "Özdemir", Company = "github" };
            var res = _mapper.Map<PersonDto>(req);
            Assert.IsTrue(
                req.Id == res.Id &&
                req.Name == res.Name &&
                req.Surname == res.Surname &&
                req.Company == res.Company);

        }

        [Test]
        public void PersonMapping_ConvertPagedResult_PersonWmToPersonDto_IsValid()
        {
            var req = new PagedResult<PersonWm> { Results = new List<PersonWm>() { new() { Id = Guid.NewGuid(), Name = "ismail", Surname = "Özdemir", Company = "github" } } };
            var res = _mapper.Map<PagedResult<PersonDto>>(req);
            Assert.IsTrue(
                req.PageNo == res.PageNo &&
                req.PageSize == res.PageSize &&
                req.TotalPageCount == res.TotalPageCount &&
                req.TotalRecordCount == res.TotalRecordCount);

        }

    }
}
