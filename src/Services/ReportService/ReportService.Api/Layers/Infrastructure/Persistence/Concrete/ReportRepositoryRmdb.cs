using ReportService.Application.Abstractions.Repositories;
using ReportService.Domain.Entities;

namespace ReportService.Persistence.Concrete
{
    public class ReportRepositoryRmdb : IReportRepository
    {
        public Task<Guid> InsertReportAsync(Report report)
        {

            throw new NotImplementedException();
        }
    }

    public class ReportRepositoryMongo : IReportRepository
    {
        public Task<Guid> InsertReportAsync(Report report)
        {

            throw new NotImplementedException();
        }
    }




}
