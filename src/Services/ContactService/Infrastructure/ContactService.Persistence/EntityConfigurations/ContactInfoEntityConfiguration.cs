using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ContactService.Core.Domain.Entities;

namespace ContactService.Persistence.EntityConfigurations
{
    internal class ContactInformationEntityConfiguration : IEntityTypeConfiguration<ContactInformation>
    {
        public void Configure(EntityTypeBuilder<ContactInformation> builder)
        {

            builder.ToTable("ContactInformations");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.InformationType);
            //.HasConversion(
            //    p => p.ToString(),
            //    p => (InformationType)Enum.Parse(typeof(InformationType), p)
            //);

            builder.HasOne(p => p.Person)
            .WithMany(p => p.ContactInformations)
            .HasForeignKey(p => p.PersonId);

        }
    }

}
