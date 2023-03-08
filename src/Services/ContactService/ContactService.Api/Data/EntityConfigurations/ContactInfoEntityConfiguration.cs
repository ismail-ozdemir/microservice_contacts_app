using ContactService.Api.Data.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ContactService.Api.Data.EntityConfigurations
{
    public class ContactInformationEntityConfiguration : IEntityTypeConfiguration<ContactInformation>
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

            builder.HasOne(p => p.User)
            .WithMany(p => p.ContactInformations)
            .HasForeignKey(p => p.UserId);

        }
    }

}
