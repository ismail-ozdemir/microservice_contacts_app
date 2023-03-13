using ContactService.Domain.Entities;
using ContactService.Persistence.Concrete.Repositories;
using ContactService.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Concrete.Repositories
{
    public class PersonRepository_UnitTest
    {

        private PersonRepository _personRepository;
        [SetUp]
        public void Setup()
        {
            _personRepository = new PersonRepository(GetContactDbContext());
        }

        [Test]
        public async Task PersonRepository_AddPerson_NewPerson()
        {
            var person = new Person { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var response = await _personRepository.AddAsync(person);
            Assert.IsTrue(response.Id != Guid.Empty && response.Name == person.Name && response.Surname == person.Surname && response.Company == person.Company);
        }




        private static ContactsContext GetContactDbContext()
        {

            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();


            var builder = new DbContextOptionsBuilder<ContactsContext>();
            builder.UseInMemoryDatabase("MyInMemoryDatabseName")
                   .UseInternalServiceProvider(serviceProvider);

            var context = new ContactsContext(builder.Options);
            return context;
        }

    }
}
