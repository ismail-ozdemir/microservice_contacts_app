using ContactService.Application.Interfaces;
using ContactService.Core.Domain.Entities;
using ContactService.Domain.Common;
using Microsoft.EntityFrameworkCore;


namespace ContactService.Persistence.Context
{
    // interface ile soyutlanırsa seed context üzerinden test yazarken işlemler daha kolay olacaktır.
    internal class ContactsContext : DbContext
    {

        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {
            
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : BaseEntity, new() => Set<TEntity>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
