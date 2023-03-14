using ContactService.Application.Filters.PersonFilters;
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
        private IEnumerable<Person> fakeData;
        [SetUp]
        public async Task Setup()
        {

            var context = GetContactDbContext();
            fakeData = GetFakePersons();
            await context.Persons.AddRangeAsync(fakeData);
            await context.SaveChangesAsync();
            _personRepository = new PersonRepository(context);
        }

        [Test]
        public async Task PersonRepository_AddPerson_NewPerson()
        {
            var person = new Person { Name = "ismail", Surname = "Özdemir", Company = "github" };
            var response = await _personRepository.AddAsync(person);
            Assert.IsTrue(response.Id != Guid.Empty && response.Name == person.Name && response.Surname == person.Surname && response.Company == person.Company);
        }

        [Test]
        public async Task PersonRepository_RemovePerson_Success()
        {

            var context = GetContactDbContext();
            await context.Persons.AddRangeAsync(fakeData);
            await context.SaveChangesAsync();

            var repo = new PersonRepository(context);
            var person = fakeData.First();

            await repo.RemoveAsync(person);
            Assert.Pass();

        }



        [Test]
        public async Task PersonRepository_GetPersonsAsync_PaginatedList()
        {

            var context = GetContactDbContext();
            await context.Persons.AddRangeAsync(fakeData);
            await context.SaveChangesAsync();

            

            var filter = new PersonFilter() { PageNo = 1, PageSize = 5 };

            var result = await _personRepository.GetPersonsAsync(filter, CancellationToken.None);

            Assert.IsNotNull(result);
            
            Assert.That(result.PageNo, Is.EqualTo(1));
            Assert.That(result.PageSize, Is.EqualTo(5));
            Assert.That(result.Results.Count,Is.EqualTo(5));
            Assert.That(result.TotalPageCount, Is.EqualTo(3));
            Assert.That(result.TotalRecordCount, Is.EqualTo(12));
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

        private IEnumerable<Person> GetFakePersons()
        {
            return new List<Person>() {
                new Person { Id = Guid.NewGuid(), Name = "ismail", Surname = "özdemir", Company = "github" },
                new Person { Id = Guid.NewGuid(), Name = "tn1", Surname = "tsn1", Company = "tc1" },
                new Person { Id = Guid.NewGuid(), Name = "tn2", Surname = "tsn1", Company = "tc1" },
                new Person { Id = Guid.NewGuid(), Name = "tn3", Surname = "tsn1", Company = "tc1" },
                new Person { Id = Guid.NewGuid(), Name = "tn4", Surname = "tsn1", Company = "github" },
                new Person { Id = Guid.NewGuid(), Name = "tn5", Surname = "tsn1", Company = "tc1" },
                new Person { Id = Guid.NewGuid(), Name = "tn6", Surname = "tsn1", Company = "tc1" },
                new Person { Id = Guid.NewGuid(), Name = "tn7", Surname = "tsn1", Company = "github" },
                new Person { Id = Guid.NewGuid(), Name = "tn8", Surname = "tsn1", Company = "tc1" },
                new Person { Id = Guid.NewGuid(), Name = "tn9", Surname = "tsn1", Company = "tc1" },
                new Person { Id = Guid.NewGuid(), Name = "tn10", Surname = "tsn1", Company = "github" },
                new Person { Id = Guid.NewGuid(), Name = "tn11", Surname = "tsn1", Company = "tc1" }
            };

        }

    }
}
