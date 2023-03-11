using ContactService.Application.Interfaces;
using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly IContactsContext _context;

        public GenericRepository(IContactsContext context)
        {
            _context = context;
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync() => await _context.SetEntity<T>().ToListAsync();

        public async virtual Task<T?> GetByIdAsync(Guid id) => await _context.SetEntity<T>().FindAsync(id);
    }
}
