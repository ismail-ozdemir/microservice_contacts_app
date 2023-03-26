using Common.Shared.Helpers;
using Common.Shared.Wrappers;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Domain.Entities;
using ContactService.Persistence.Context;
using ContactService.Shared.Filters;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Persistence.Concrete.Repositories
{
    internal class PersonRepository : GenericRepository<Person>, IPersonRepository
    {
        public PersonRepository(ContactsContext context) : base(context)
        {
            
        }


        public async Task<PagedResult<PersonWm>> GetPersonsAsync(PersonFilter filter, CancellationToken cancellationToken)
        {
            IQueryable<PersonWm> query = _context.Persons.AsNoTracking()
                               .Select(p => new PersonWm
                               {
                                   Id = p.Id,
                                   Name = p.Name,
                                   Surname = p.Surname,
                                   Company = p.Company
                               });

            return await PaginationHelper.GetPagedAsync(query: query, filter.PageNo, filter.PageSize);
        }


        public Task<bool> CheckedPersonByIdAsync(Guid PersonId)
        {
            return Task.FromResult(_context.Persons.Any(x => x.Id == PersonId));
        }
    }
}
