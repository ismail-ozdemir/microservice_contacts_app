
using BuildingBlocks.EventBus.Absractions;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Application.Abstractions.Services;
using ReportService.Domain.Enums;

namespace ReportService.Application.Events
{
    public class CreateReportRequestEventHandler : IQueueEventHandler<CreateReportRequestEvent>
    {

        private readonly IReportRepository _reportRepository;
        private readonly IContactService _contactService;
        private readonly ILogger<CreateReportRequestEventHandler> _logger;
        private readonly IFileService _fileService;
        public CreateReportRequestEventHandler(IReportRepository reportRepository, IContactService contactService, ILogger<CreateReportRequestEventHandler> logger, IFileService fileService)
        {
            _reportRepository = reportRepository;
            _contactService = contactService;
            _logger = logger;
            _fileService = fileService;
        }

        public async Task Handle(CreateReportRequestEvent @event)
        {
            _logger.LogInformation("{EventHandler} handle method triggred .Report consumed from queue. ReportId :{ReportId}", nameof(CreateReportRequestEventHandler), @event.Id);

            var report = await _reportRepository.GetReportByIdAsync(@event.Id, CancellationToken.None);
            if (report != null)
            {

                if (report.ReportType == Domain.Enums.ReportType.LocationPersonAndPhoneNumberCountReport)
                {
                    _logger.LogInformation("Report processing started");
                    report.ReportStatusType = Domain.Enums.ReportStatusType.Processing;
                    await _reportRepository.UpdateReportAsync(report, CancellationToken.None);
                    _logger.LogInformation("Update report status on database");

                    var grpcData = await _contactService.GetPhoneNumberAndPersonCountByLocation(CancellationToken.None);
                    var now = DateTime.UtcNow;

                    string fileName = $"{report.ReportType}_{now.ToString("dd_MM_yyyy_HHmmss")}";
                    string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ExcelReports");
                    _logger.LogInformation("Report creating");
                    report.FilePath = _fileService.SaveDataExcelFormat(grpcData, folderPath, fileName);
                    _logger.LogInformation("Report created. FilePath : {FilePath}", report.FilePath);
                    report.ReportStatusType = Domain.Enums.ReportStatusType.Completed;
                    report.CompletedDate = now;

                    await _reportRepository.UpdateReportAsync(report, CancellationToken.None);
                    _logger.LogInformation("Update report status on database.");
                }
            }
            else
                _logger.LogInformation("Report not found in database");

        }
    }
}
