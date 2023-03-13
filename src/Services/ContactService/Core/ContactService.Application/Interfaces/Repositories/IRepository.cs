using ContactService.Domain.Common;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(bool isTracking = false);

    }
}
