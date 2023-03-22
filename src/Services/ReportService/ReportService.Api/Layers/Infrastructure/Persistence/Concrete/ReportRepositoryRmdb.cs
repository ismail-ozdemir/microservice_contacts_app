
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
        public async Task<Report?> GetReportByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Reports.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }
        public async Task<Report> UpdateReportAsync(Report report, CancellationToken cancellationToken)
        {
            _context.Reports.Update(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Guid> InsertReportAsync(Report report, CancellationToken cancellationToken)
        {
            await _context.Reports.AddAsync(report, cancellationToken);
            await _context.SaveChangesAsync();
            return report.Id;

        }


    }

}
