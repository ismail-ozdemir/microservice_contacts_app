using Common.Shared.Wrappers;
using ContactService.Application.ViewModels;
using ContactService.Domain.Entities;
using ContactService.Shared.Filters;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IContactInfoRepository : IRepository<ContactInformation>
    {
        Task<PagedResult<ContactInfoWm>> GetContactInfoListByPersonAsync(PersonFilter.ById filter);

        Task<List<ContactReportByLocationVm>> GetContactReportByLocation();

    }
}
