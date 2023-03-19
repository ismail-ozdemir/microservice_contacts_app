using ReportService.Domain.Enums;

namespace ReportService.Domain.Entities
{
    public class Report
    {
        public Guid Id { get; set; }
        public ReportStatusType ReportStatusType { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public ReportType ReportType { get; set; }
        public string FileUrl { get; set; } = string.Empty;

    }
}
