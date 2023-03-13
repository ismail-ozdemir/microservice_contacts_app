using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ContactService.Domain.Entities;

namespace ContactService.Persistence.EntityConfigurations
{
    internal class PersonEntityConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {

            builder.ToTable("Persons");

            builder.HasKey(x => x.Id);


            builder.Property(ci => ci.Name)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(ci => ci.Surname)
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(ci => ci.Company)
                   .HasMaxLength(100)
                   .IsRequired();

        }
    }

}
