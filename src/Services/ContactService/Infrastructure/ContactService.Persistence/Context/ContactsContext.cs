using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ContactService.Persistence.Context
{
    [ExcludeFromCodeCoverage]
    internal class ContactsContext : DbContext
    {

        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
