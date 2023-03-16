using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
using ContactService.Application.ViewModels;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IContactInfoRepository
    {
        Task<PagedResult<ContactInfoWm>> GetContactInfoListByPersonAsync(PersonFilter.ById filter);

    }
}
