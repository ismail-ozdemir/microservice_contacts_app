using AutoMapper;
using Common.Shared.Wrappers;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Mapping;
using ContactService.Application.ViewModels;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Domain.Entities;
using ContactService.Shared.Dto.ContactInfoDtos;
using ContactService.Shared.Dto.PersonDtos;

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
            var request = new CreatePersonRequest() { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var person = _mapper.Map<Person>(request);
            
            Assert.IsTrue(request.Name == person.Name && request.Surname == person.Surname && request.Company == person.Company);

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
            var res = _mapper.Map<PersonResponse>(req);
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
            var res = _mapper.Map<PagedResult<PersonResponse>>(req);
            Assert.IsTrue(
                req.PageNo == res.PageNo &&
                req.PageSize == res.PageSize &&
                req.TotalPageCount == res.TotalPageCount &&
                req.TotalRecordCount == res.TotalRecordCount);

        }



        [Test]
        public void PersonMapping_Convert_ContactInfoWm_To_ContactInfoDto_IsValid()
        {
            ContactInfoWm req = new() { InfoId = Guid.NewGuid(), InfoType = "phone", InfoContent = "05551112233" };

            var res = _mapper.Map<ContactInfoResponseDto>(req);

            Assert.IsNotNull(req);
            Assert.IsTrue(req.InfoId == res.InfoId);
            Assert.IsTrue(req.InfoType == res.InfoType);
            Assert.IsTrue(req.InfoContent == res.InfoDetail);

        }

    }
}
