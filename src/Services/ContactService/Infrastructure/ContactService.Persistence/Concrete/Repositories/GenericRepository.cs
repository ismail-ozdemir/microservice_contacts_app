using ContactService.Application.Interfaces.Repository;
using ContactService.Domain.Common;
using ContactService.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Persistence.Concrete.Repositories
{
    internal class GenericRepository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly ContactsContext _context;
        protected readonly DbSet<T> Table;
        public GenericRepository(ContactsContext context)
        {
            _context = context;
            Table = _context.Set<T>();
        }

        public async virtual Task<IEnumerable<T>> GetAllAsync(bool isTracking = false)
        {
            return isTracking ? await Table.ToListAsync() : await Table.AsNoTracking().ToListAsync();
        }

        public async virtual Task<T> AddAsync(T entity)
        {
            await Table.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task RemoveAsync(T entity)
        {
            var result = Table.Remove(entity);
            var r = await _context.SaveChangesAsync();

        }

        public async virtual Task<T?> GetByIdAsync(Guid id) => await Table.FindAsync(id);

    }
}
