using AutoMapper;
using Common.Shared.Wrappers;
using ContactService.Application.Features.PersonFeatures.Queries;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Mapping;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Shared.Filters;
using Moq;


namespace Features.PersonFeatures.Queries
{
    public class GetPersonListQueryHandler_UnitTest
    {

        private IMapper _mapper;
        private IPersonRepository _personRepository;


        [SetUp]
        public void Setup()
        {
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<PersonMapping>(); }).CreateMapper();
            _personRepository = CreatePersonRepository();
        }

        [Test, TestCaseSource(nameof(ConstractureArgsCase))]
        public void GetPersonListQueryHandler_CreateWithNullConstuctureParameters_ArgumentNullException(IPersonRepository pr, IMapper mp)
        {
            var ex = Assert.Catch(() => { new GetPersonListQueryHandler(personRepository: pr, mapper: mp); });
            Assert.IsNotNull(ex);
            Assert.That(ex.GetType(), Is.EqualTo(typeof(ArgumentNullException)), $"beklenen hata türü {nameof(ArgumentNullException)}");

        }


        [Test]
        public async Task GetPersonListQueryHandler_HandleGetPersonListQuery_PagedResult__PersonDto()
        {
            GetPersonListQueryHandler handle = new(personRepository: _personRepository, mapper: _mapper);

            var result = await handle.Handle(new(new() { PageNo = 1, PageSize = 10 }), CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Results);
            Assert.That(result.Results.Count, Is.EqualTo(3));
        }


        private IPersonRepository CreatePersonRepository()
        {
            Mock<IPersonRepository> mock = new();
            mock.Setup(pr => pr.GetPersonsAsync(It.IsAny<PersonFilter>(), It.IsAny<CancellationToken>()))
                .Returns<PersonFilter, CancellationToken>((filter, token) => Task.FromResult(
                    new PagedResult<PersonWm>()
                    {
                        PageNo = filter.PageNo,
                        PageSize = filter.PageSize,
                        Results = new List<PersonWm>() {
                            new() { Id = Guid.NewGuid(), Name = "ismail", Surname = "özdemir", Company = "github" },
                            new() { Id = Guid.NewGuid(), Name = "t2", Surname = "özdemir", Company = "github" },
                            new() { Id = Guid.NewGuid(), Name = "t3", Surname = "özdemir", Company = "github" },
                        },
                        TotalPageCount = 1,
                        TotalRecordCount = 3

                    }
                    ));
            return mock.Object;
        }

        public static object[] ConstractureArgsCase =
     {
                new object[] { null, new Mock<IMapper>().Object },
                new object[] { new Mock<IPersonRepository>().Object, null }

        };


    }
}
