using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
using ContactService.Application.Interfaces.Repository;
using ContactService.Application.ViewModels;
using ContactService.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Persistence.Concrete.Repositories
{
    internal class ContactInfoRepository : IContactInfoRepository
    {
        private readonly ContactsContext _context;
        public ContactInfoRepository(ContactsContext contactsContext)
        {
            _context = contactsContext ?? throw new ArgumentNullException(nameof(contactsContext));
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
    }
}
