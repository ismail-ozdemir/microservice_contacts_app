
using AutoMapper;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.Mapping;
using ContactService.Domain.Entities;
using Moq;

namespace ContactService.Application.UnitTest.Features.PersonFeatures.Commands
{
    public class AddPersonCommandHandler_UnitTest
    {

        private IMapper _mapper;
        private IPersonRepository _personRepository;

        private CreatePersonRequest req;

        [SetUp]
        public void Setup()
        {
            _mapper = new MapperConfiguration(cfg => { cfg.AddProfile<PersonMapping>(); }).CreateMapper();
            req = new() { Name = "ismail", Surname = "Özdemir", Company = "github" };
            _personRepository = CreatePersonRepository();
        }

        [Test]
        public async Task AddPersonCommandHandler_HandleAddPersonCommand_CreatePersonResponse()
        {

            AddPersonCommand command = new(req);
            AddPersonCommandHandler handler = new AddPersonCommandHandler(_mapper, _personRepository);
            var result = await handler.Handle(command, new CancellationToken());

            Assert.IsNotNull(result);
            Assert.IsTrue(result.PersonId != Guid.Empty && result.Name == req.Name && result.Surname == req.Surname && result.Company == req.Company);
        }


        private IPersonRepository CreatePersonRepository()
        {
            Mock<IPersonRepository> mock = new();
            mock.Setup(pr => pr.AddAsync(It.IsAny<Person>()))
                .Returns<Person>((person) => Task.FromResult(new Person { Id = Guid.NewGuid(), Name = person.Name, Surname = person.Surname, Company = person.Company }));
            return mock.Object;
        }
    }
}
