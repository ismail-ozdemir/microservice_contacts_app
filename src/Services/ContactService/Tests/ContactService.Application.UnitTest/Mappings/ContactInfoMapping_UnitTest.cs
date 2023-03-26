using AutoMapper;
using ContactService.Application.Mapping;
using ContactService.Domain;
using ContactService.Domain.Entities;
using ContactService.Shared.Dto.ContactInfoDtos;

namespace Mappings
{
    public class ContactInfoMapping_UnitTest
    {

        private MapperConfiguration config;
        private IMapper _mapper;
        [SetUp]
        public void Setup()
        {
            config = new MapperConfiguration(cfg => cfg.AddProfile<ContactInfoMapping>());
            _mapper = config.CreateMapper();

        }


        [Test]
        public void ContactInfoMapping_Configuration_IsValid()
        {
            config.AssertConfigurationIsValid();
        }


        [Test]
        public void ContactInfoMapping_Convert_ContactInformation_To_SaveContactInfoResponseDto()
        {
            var conf = new ContactInformation { Id = Guid.NewGuid(), PersonId = Guid.NewGuid(), InformationType = InformationType.Phone, Content = "test" };
            var result = _mapper.Map<SaveContactInfoResponseDto>(conf);
            Assert.IsNotNull(result);
        }


        [Test]
        public void ContactInfoMapping_Convert_InsertContactInfoRequest_To_ContactInformation()
        {
            var conf = new InsertContactInfoRequest { PersonId = Guid.NewGuid(), InfoType = "Phone", InfoContent = "test" };
            var result = _mapper.Map<ContactInformation>(conf);
            Assert.IsNotNull(result);
        }


    }
}
