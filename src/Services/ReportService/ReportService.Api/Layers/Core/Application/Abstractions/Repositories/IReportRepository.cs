using ReportService.Domain.Entities;

namespace ReportService.Application.Abstractions.Repositories
{
    public interface IReportRepository
    {
        //TODO sayfalı  bir şekilde gelmeli
        public Task<List<Report>> GetReportsAsync(CancellationToken cancellicationToken);
        /// <summary>
        /// Veritabanına rapor isteğini kaydeder.
        /// </summary>
        /// <param name="report">Veri tabanına kaydedilecek olan rapor nesnesi</param>
        /// <returns>
        /// Veritabanına kaydedilen raporun Id deperini dönderir.
        /// </returns>
        public Task<Guid> InsertReportAsync(Report report, CancellationToken cancellationToken);

        public Task<Report> UpdateReportAsync(Report report, CancellationToken cancellationToken);



        public Task<Report?> GetReportByIdAsync(Guid Id, CancellationToken cancellationToken);


    }
}
