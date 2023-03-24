using Microsoft.EntityFrameworkCore;
using ReportService.Domain.Entities;

namespace ReportService.Persistence.Contexts
{

    internal class ReportContextRmdb : DbContext
    {

        public ReportContextRmdb(DbContextOptions<ReportContextRmdb> options) : base(options)
        {
        }
        public DbSet<Report> Reports { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

    }
}
