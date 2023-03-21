
using Microsoft.EntityFrameworkCore;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Domain.Entities;
using ReportService.Persistence.Contexts;

namespace ReportService.Persistence.Concrete
{
    internal class ReportRepositoryRmdb : IReportRepository
    {
        private readonly ReportContextRmdb _context;

        public ReportRepositoryRmdb(ReportContextRmdb context)
        {
            _context = context;
        }

        public async Task<List<Report>> GetReportsAsync(CancellationToken cancellicationToken)
        {
            return await _context.Reports.AsNoTracking().ToListAsync(cancellicationToken);
        }

        public async Task<Guid> InsertReportAsync(Report report, CancellationToken cancellationToken)
        {
            await _context.Reports.AddAsync(report, cancellationToken);
            await _context.SaveChangesAsync();
            return report.Id;

        }
    }

}
