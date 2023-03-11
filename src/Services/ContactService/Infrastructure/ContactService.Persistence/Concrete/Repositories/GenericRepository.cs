using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Common;
using ContactService.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Persistence.Concrete.Repositories
{
    internal class GenericRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly ContactsContext _context;
        public GenericRepository(ContactsContext context) => _context = context;

        public async virtual Task<IEnumerable<T>> GetAllAsync() => await _context.SetEntity<T>().AsNoTracking().ToListAsync();

        public async virtual Task<bool> AddAsync(T entity) => (await _context.SetEntity<T>().AddAsync(entity)).State == EntityState.Added;
        public Task<bool> RemoveAsync(T entity) => Task.FromResult(_context.Set<T>().Remove(entity).State == EntityState.Deleted);

        public async virtual Task<T?> GetByIdAsync(Guid id) => await _context.SetEntity<T>().FindAsync(id);

        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}
