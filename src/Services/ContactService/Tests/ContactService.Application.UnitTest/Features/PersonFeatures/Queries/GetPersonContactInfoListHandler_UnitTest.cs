using AutoMapper;
using ContactService.Application.Exceptions;
using ContactService.Application.Features.PersonFeatures.Queries;
using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Mapping;
using ContactService.Application.ViewModels;
using ContactService.Domain.Entities;
using Moq;
using NUnit.Framework;
using static ContactService.Application.Features.PersonFeatures.Queries.GetPersonContactInfoList;

namespace ContactService.Application.UnitTest.Features.PersonFeatures.Queries
{
    internal class GetPersonContactInfoListHandler_UnitTest
    {

        private IMapper _mapper;
        private IContactInfoRepository _contactRepo;
        private IPersonRepository _personRepo;


        [SetUp]
        public void Setup()
        {
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<PersonMapping>(); }).CreateMapper();
            _personRepo = GetPersonFakeRepo();
            _contactRepo = GetFakeContactRepo();
        }



        [Test]
        public void GetPersonContactInfoListHandler_CreateConstructerArguments_Success()
        {
            var result = new GetPersonContactInfoListHandler(new Mock<IMapper>().Object, new Mock<IPersonRepository>().Object, new Mock<IContactInfoRepository>().Object);
            Assert.IsNotNull(result);
        }


        [Test, TestCaseSource(nameof(ConstractureArgsCase))]
        public void GetPersonContactInfoListHandler_CreateWithNullConstuctureParameters_ArgumentNullException(IMapper mp, IPersonRepository personRepo, IContactInfoRepository contRepo, string expect)
        {
            var ex = Assert.Catch(() => { new GetPersonContactInfoListHandler(personRepository: personRepo, contactInfoRepository: contRepo, mapper: mp); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(typeof(ArgumentNullException)), $"beklenen hata türü {nameof(ArgumentNullException)}");
            Assert.That((ex as ArgumentNullException)!.ParamName, Is.EqualTo(expect));

        }

        [Test, TestCaseSource(nameof(HandleExceptionCase))]
        public void GetPersonContactInfoListHandler_HandleGetPersonContactInfoList_Exception(GetPersonContactInfoList req, Type expectedException)
        {
            var handler = new GetPersonContactInfoListHandler(_mapper, _personRepo, _contactRepo);
            var ex = Assert.CatchAsync(async () => { await handler.Handle(req, CancellationToken.None); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(expectedException));
        }

        [Test]
        public async Task GetPersonContactInfoListHandler_HandleGetPersonContactInfoList_Success()
        {
            var req = new GetPersonContactInfoList(new PersonFilter.ById { Id = Guid.NewGuid() });
            var handler = new GetPersonContactInfoListHandler(_mapper, _personRepo, _contactRepo);
            var result = await handler.Handle(req, CancellationToken.None);
            Assert.IsNotNull(result);

        }




        private static object[] ConstractureArgsCase =
        {
                new object[] { null,null,null, "mapper"},
                new object[] { new Mock<IMapper>().Object,null,null,"personRepository" },
                new object[] { new Mock<IMapper>().Object,new Mock<IPersonRepository>().Object,null,"contactInfoRepository" }
        };


        private static object[] HandleExceptionCase =
        {
                new object[] { null,typeof(ArgumentNullException)},
                new object[] { new GetPersonContactInfoList(new()),typeof(RecordNotFoundException)}
        };







        private IPersonRepository GetPersonFakeRepo()
        {
            var mock = new Mock<IPersonRepository>();

            mock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
                .Returns<Guid>((id) =>
                {
                    return id == Guid.Empty ? Task.FromResult<Person?>(null)
                                         : Task.FromResult<Person?>(new Person() { Id = id });

                });



            return mock.Object;
        }
        private IContactInfoRepository GetFakeContactRepo()
        {
            var mock = new Mock<IContactInfoRepository>();

            mock.Setup(m => m.GetContactInfoListByPersonAsync(It.IsAny<PersonFilter.ById>()))
                .ReturnsAsync<PersonFilter.ById, IContactInfoRepository, PagedResult<ContactInfoWm>>((filter) =>
                {
                    var result = new PagedResult<ContactInfoWm>();
                    result.Results.Add(new ContactInfoWm { InfoId = Guid.NewGuid(), InfoContent = "ankara", InfoType = "location" });
                    return result;
                });
            return mock.Object;
        }





    }
}
