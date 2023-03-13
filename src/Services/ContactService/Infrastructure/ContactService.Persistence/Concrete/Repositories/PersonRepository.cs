using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Entities;
using ContactService.Persistence.Context;

namespace ContactService.Persistence.Concrete.Repositories
{
    internal class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ContactsContext context) : base(context)
        {
        }

    }
}
