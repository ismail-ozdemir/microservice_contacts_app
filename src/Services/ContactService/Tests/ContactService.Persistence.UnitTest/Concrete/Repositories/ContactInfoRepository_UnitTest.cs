using ContactService.Domain;
using ContactService.Domain.Entities;
using ContactService.Persistence.Concrete.Repositories;
using ContactService.Persistence.Context;
using ContactService.Persistence.UnitTest.Helper;
using ContactService.Shared.Filters;

namespace Concrete.Repositories
{
    public class ContactInfoRepository_UnitTest
    {

        private ContactInfoRepository _contactInfoRepository;

        [SetUp]
        public async Task Setup()
        {

            var context = ConfigurationHelper.GetFakeContactDbContext();
            await FillData(context);
            _contactInfoRepository = new ContactInfoRepository(context);
        }



        [Test]
        public async Task ContactInfoRepository_GetContactInfoListByPersonAsync_Success()
        {
            var filter = new PersonFilter.ById { Id = Guid.Parse("61f84982-2cb9-4197-9fc2-d9de3ad0a972"), PageSize = 10 };
            var result = await _contactInfoRepository.GetContactInfoListByPersonAsync(filter);
            Assert.IsNotNull(result);
            Assert.That(result.Results.Count, Is.EqualTo(3));
        }





        private async Task FillData(ContactsContext context)
        {

            List<Person> personList = new List<Person> {
                new Person { Id = Guid.Parse("61f84982-2cb9-4197-9fc2-d9de3ad0a972"), Name = "ismail", Surname = "özdemir", Company = "github"},
                new Person { Id = Guid.Parse("44e28757-a428-4e61-bee5-93953299506a"), Name = "test", Surname = "user", Company = "gitlab"},
            };
            await context.Persons.AddRangeAsync(personList);


            List<ContactInformation> fakeCont = new() {
                new (){Id=Guid.NewGuid(),PersonId=Guid.Parse("61f84982-2cb9-4197-9fc2-d9de3ad0a972"),InformationType=InformationType.Phone,Content="0544XXXyyZZ" },
                new (){Id=Guid.NewGuid(),PersonId=Guid.Parse("61f84982-2cb9-4197-9fc2-d9de3ad0a972"),InformationType=InformationType.Email,Content="ismail@ozdemir.com" },
                new (){Id=Guid.NewGuid(),PersonId=Guid.Parse("61f84982-2cb9-4197-9fc2-d9de3ad0a972"),InformationType=InformationType.Location,Content="Ankara" },

                new (){Id=Guid.NewGuid(),PersonId=Guid.Parse("44e28757-a428-4e61-bee5-93953299506a"),InformationType=InformationType.Phone,Content="0544XXXyyZZ" },
                new (){Id=Guid.NewGuid(),PersonId=Guid.Parse("44e28757-a428-4e61-bee5-93953299506a"),InformationType=InformationType.Email,Content="test@user.com" },
                new (){Id=Guid.NewGuid(),PersonId=Guid.Parse("44e28757-a428-4e61-bee5-93953299506a"),InformationType=InformationType.Location,Content="Istanbul" },
            };

            await context.ContactInformations.AddRangeAsync(fakeCont);


            await context.SaveChangesAsync();

        }

    }
}
