namespace ReportService.Application.Dtos
{
    public class CreateReportCommandResponseDto
    {
        public Guid ReportId { get; set; }

        public CreateReportCommandResponseDto(Guid reportId)
        {
            ReportId = reportId;
        }
    }
}
