using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ContactService.Core.Domain.Entities;

namespace ContactService.Persistence.EntityConfigurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder.ToTable("Users");

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
