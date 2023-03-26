using Common.Shared.Helpers;
using Common.Shared.Wrappers;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.ViewModels;
using ContactService.Domain;
using ContactService.Domain.Entities;
using ContactService.Persistence.Context;
using ContactService.Shared.Filters;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactService.Persistence.Concrete.Repositories
{
    internal class ContactInfoRepository : GenericRepository<ContactInformation>, IContactInfoRepository
    {
        public ContactInfoRepository(ContactsContext contactsContext) : base(contactsContext)
        {

        }

        public async Task<PagedResult<ContactInfoWm>> GetContactInfoListByPersonAsync(PersonFilter.ById filter)
        {
            var query = _context.ContactInformations.AsNoTracking()
                .Where(r => r.PersonId == filter.Id)
                .Select(x => new ContactInfoWm
                {
                    InfoId = x.Id,
                    InfoType = x.InformationType.ToString(),
                    InfoContent = x.Content
                });

            return await PaginationHelper.GetPagedAsync(query, filter.PageNo, filter.PageSize);
        }

        public async Task<List<ContactReportByLocationVm>> GetContactReportByLocation()
        {

            Expression<Func<ContactInformation, bool>> locationExpr = (ci) => ci.InformationType == InformationType.Location;

            var locationPersonCount = await _context.ContactInformations.AsNoTracking()
                                        .Where(locationExpr)
                                        .GroupBy(x => x.Content)
                                        .Select(group => new ContactReportByLocationVm
                                        {
                                            LocationName = group.Key,
                                            PersonCountInLocation = group.Count(),
                                            //PhoneNumberCountInLocation = group.Sum(a => _context.ContactInformations.Where(s => s.InformationType == InformationType.Phone && s.PersonId == a.PersonId).Count()),
                                            PhoneNumberCountInLocation = _context.ContactInformations.Join(_context.ContactInformations.Where(ci => ci.InformationType == InformationType.Phone),
                                                        location => location.PersonId,
                                                        phone => phone.PersonId,
                                                        (location, phone) => new { location, phone }
                                                        ).Where(x => x.location.Content == group.Key).Count()
                                        }).ToListAsync();

            return locationPersonCount;
        }
    }
}
