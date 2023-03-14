using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Domain.Entities;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<PagedResult<PersonWm>> GetPersonsAsync(PersonFilter filter, CancellationToken cancellationToken);
    }
}
