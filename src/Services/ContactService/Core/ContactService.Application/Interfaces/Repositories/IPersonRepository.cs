using Common.Shared.Wrappers;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Domain.Entities;
using ContactService.Shared.Filters;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<PagedResult<PersonWm>> GetPersonsAsync(PersonFilter filter, CancellationToken cancellationToken);
        /// <summary>
        /// ilgili kaydın var olup olmadığı bilgisini döner
        /// </summary>
        /// <returns></returns>
        Task<bool> CheckedPersonByIdAsync(Guid PersonId);
    }
}
