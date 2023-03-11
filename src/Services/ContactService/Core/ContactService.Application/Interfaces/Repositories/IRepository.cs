using ContactService.Domain.Common;

namespace ContactService.Application.Interfaces.Repository
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
