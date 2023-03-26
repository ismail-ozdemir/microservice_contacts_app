using AutoMapper;
using Common.Shared.Exceptions;
using Common.Shared.Wrappers;
using ContactService.Application.Features.ContactInfoFeatures;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Mapping;
using ContactService.Application.ViewModels;
using ContactService.Domain.Entities;
using ContactService.Shared.Filters;
using Moq;
using static ContactService.Application.Features.ContactInfoFeatures.InsertContactInfoCommand;

namespace ContactService.Application.UnitTest.Features.ContactInfo
{
    internal class InsertContactInfoCommandHandler_UnitTest
    {

        private IMapper _mapper;
        private IContactInfoRepository _contactRepo;
        private IPersonRepository _personRepo;


        [SetUp]
        public void Setup()
        {
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<ContactInfoMapping>(); }).CreateMapper();
            _personRepo = GetPersonFakeRepo();
            _contactRepo = GetFakeContactRepo();
        }


        [Test, TestCaseSource(nameof(ConstractureArgsCase))]
        public void InsertContactInfoCommandHandler_CreateWithNullConstuctureParameters_ArgumentNullException(IMapper mp, IPersonRepository pr, IContactInfoRepository cr, string expect)
        {
            var ex = Assert.Catch(() => { new InsertContactInfoCommandHandler(mp, cr, pr); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(typeof(ArgumentNullException)), $"beklenen hata türü {nameof(ArgumentNullException)}");
            Assert.That((ex as ArgumentNullException)!.ParamName, Is.EqualTo(expect));

        }

        [Test, TestCaseSource(nameof(HandleExceptionCase))]
        public void InsertContactInfoCommandHandler_HandleInsertContactInfoCommand_Exception(InsertContactInfoCommand command, Type expectedException)
        {
            var handler = new InsertContactInfoCommandHandler(_mapper, _contactRepo, _personRepo);
            var ex = Assert.CatchAsync(async () => { await handler.Handle(command, CancellationToken.None); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(expectedException));
        }


        [Test]
        public async Task InsertContactInfoCommandHandler_HandleInsertContactInfoCommand_Success()
        {
            var command = new InsertContactInfoCommand(new() { PersonId = Guid.NewGuid(), InfoType = "Phone", InfoContent = "0555aaaBBcc" });
            var handler = new InsertContactInfoCommandHandler(_mapper, _contactRepo, _personRepo);
            var result = await handler.Handle(command, CancellationToken.None);
            Assert.IsNotNull(result);
        }



        private static IPersonRepository GetPersonFakeRepo()
        {
            var mock = new Mock<IPersonRepository>();

            mock.Setup(m => m.CheckedPersonByIdAsync(It.IsAny<Guid>()))
                .Returns<Guid>((id) => Task.FromResult<bool>(id != Guid.Empty));



            return mock.Object;
        }
        private static IContactInfoRepository GetFakeContactRepo()
        {
            var mock = new Mock<IContactInfoRepository>();

            mock.Setup(m => m.GetContactInfoListByPersonAsync(It.IsAny<PersonFilter.ById>()))
                .ReturnsAsync<PersonFilter.ById, IContactInfoRepository, PagedResult<ContactInfoWm>>((filter) =>
                {
                    var result = new PagedResult<ContactInfoWm>();
                    result.Results.Add(new ContactInfoWm { InfoId = Guid.NewGuid(), InfoContent = "ankara", InfoType = "location" });
                    return result;
                });
            mock.Setup(pr => pr.AddAsync(It.IsAny<ContactInformation>()))
               .Returns<ContactInformation>((req) =>
               {
                   req.Id = Guid.NewGuid();
                   return Task.FromResult(req);
               });
            return mock.Object;
        }

        private static object[] ConstractureArgsCase =
        {
                new object[] { null,null,null, "mapper"},
                new object[] { new Mock<IMapper>().Object,null,null,"personRepository" },
                new object[] { new Mock<IMapper>().Object,new Mock<IPersonRepository>().Object,null, "contactInfoRepository" }
        };

        private static object[] HandleExceptionCase =
        {
                new object[] { null,typeof(ArgumentNullException)},
                new object[] { new InsertContactInfoCommand(new()),typeof(RecordNotFoundException)}
        };
    }
}
