using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Domain.Entities;

namespace ReportService.Persistence.EntityConfigurations
{
    public class ReportEntityConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ReportType);
            builder.Property(p => p.RequestDate).IsRequired();
            builder.Property(p => p.CompletedDate);
            builder.Property(p => p.ReportType).IsRequired();
            builder.Property(p => p.FilePath);
            

        }
    }
}
