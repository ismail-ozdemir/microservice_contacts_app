using Common.Shared.Exceptions;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Entities;
using Moq;

namespace Features.PersonFeatures.Commands
{
    public class RemovePersonCommandHandler_UnitTest
    {

        private IPersonRepository _personRepository;


        [SetUp]
        public void Setup()
        {

            _personRepository = CreatePersonRepository();
        }

        [Test]
        public void RemovePersonCommandHandler_RemovePersonCommandWithEmptyId_Exception()
        {

            RemovePersonCommandHandler handler = new(_personRepository);
            RemovePersonCommand command = new(Guid.Empty);

            Assert.ThrowsAsync<RecordNotFoundException>(() => handler.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task RemovePersonCommandHandler_RemovePersonCommandWithId_Success()
        {

            RemovePersonCommandHandler handler = new(_personRepository);
            RemovePersonCommand command = new(Guid.NewGuid());

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.IsNotEmpty(result);
        }


        private IPersonRepository CreatePersonRepository()
        {
            Mock<IPersonRepository> mock = new();
            mock.Setup(pr => pr.AddAsync(It.IsAny<Person>()))
                .Returns<Person>((person) => Task.FromResult(new Person { Id = Guid.NewGuid(), Name = person.Name, Surname = person.Surname, Company = person.Company }));

            mock.Setup(pr => pr.GetByIdAsync(It.IsAny<Guid>()))
                .Returns<Guid>((personId) => Task.FromResult(personId == Guid.Empty ? null : new Person { Id = personId, Name = "testName", Surname = "testSurname", Company = "testCompany" }));

            mock.Setup(pr => pr.RemoveAsync(It.IsAny<Person>())).Returns(Task.FromResult(true));
            return mock.Object;
        }


    }
}

