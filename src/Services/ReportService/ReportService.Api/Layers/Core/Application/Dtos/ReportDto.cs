namespace ReportService.Application.Dtos
{
    public class ReportDto
    {
        public Guid Id { get; set; }
        public string ReportType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}
