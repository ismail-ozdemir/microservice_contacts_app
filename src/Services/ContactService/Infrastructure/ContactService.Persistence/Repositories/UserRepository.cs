using ContactService.Application.Interfaces;
using ContactService.Application.Interfaces.Repository;
using ContactService.Core.Domain.Entities;

namespace ContactService.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IContactsContext context) : base(context)
        {
        }
    }
}
