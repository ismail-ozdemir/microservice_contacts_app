using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
using ContactService.Application.ViewModels;
using ContactService.Domain.Entities;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IContactInfoRepository : IRepository<ContactInformation>
    {
        Task<PagedResult<ContactInfoWm>> GetContactInfoListByPersonAsync(PersonFilter.ById filter);

        Task<ContactReportByLocationVm> GetContactReportByLocation(string LocationName);

    }
}
