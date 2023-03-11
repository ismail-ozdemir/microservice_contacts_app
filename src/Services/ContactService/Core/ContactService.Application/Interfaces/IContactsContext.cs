using ContactService.Core.Domain.Entities;
using ContactService.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Application.Interfaces
{
    public interface IContactsContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : BaseEntity, new();


    }
}
