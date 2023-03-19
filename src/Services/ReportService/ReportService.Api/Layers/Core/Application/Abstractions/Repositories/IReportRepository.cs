using ReportService.Domain.Entities;

namespace ReportService.Application.Abstractions.Repositories
{
    public interface IReportRepository
    {
        /// <summary>
        /// Veritabanına rapor isteğini kaydeder.
        /// </summary>
        /// <param name="report">Veri tabanına kaydedilecek olan rapor nesnesi</param>
        /// <returns>
        /// Veritabanına kaydedilen raporun Id deperini dönderir.
        /// </returns>
        public Task<Guid> InsertReportAsync(Report report);
    }
}
