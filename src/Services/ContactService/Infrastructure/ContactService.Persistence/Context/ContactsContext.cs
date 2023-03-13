using ContactService.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace ContactService.Persistence.Context
{
    internal class ContactsContext : DbContext
    {

        public ContactsContext(DbContextOptions<ContactsContext> options) : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<ContactInformation> ContactInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
