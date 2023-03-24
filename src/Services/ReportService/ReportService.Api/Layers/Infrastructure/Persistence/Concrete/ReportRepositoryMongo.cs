using MongoDB.Driver;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Domain.Entities;
using ReportService.Persistence.Contexts;

namespace ReportService.Persistence.Concrete
{
    public class ReportRepositoryMongo : IReportRepository
    {

        private readonly ReportContextMongo _context;

        public ReportRepositoryMongo(ReportContextMongo context)
        {
            _context = context;
        }

        public Task<Report?> GetReportByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<List<Report>> GetReportsAsync(CancellationToken cancellicationToken)
        {
            //TODO tekrar bak. 
            return Task.FromResult(_context.Reports.AsQueryable().ToList());
        }

        public async Task<Guid> InsertReportAsync(Report report, CancellationToken cancellationToken)
        {
            await _context.Reports.InsertOneAsync(report);
            return report.Id;
        }

        public Task<Report> UpdateReportAsync(Report report, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }




}
