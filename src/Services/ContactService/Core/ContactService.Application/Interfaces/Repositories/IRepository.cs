using ContactService.Domain.Common;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<bool> AddAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();

        Task<int> SaveAsync();
    }
}
