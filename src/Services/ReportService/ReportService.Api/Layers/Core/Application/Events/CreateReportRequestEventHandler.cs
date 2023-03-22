
using BuildingBlocks.EventBus.Absractions;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Application.Abstractions.Services;

namespace ReportService.Application.Events
{
    public class CreateReportRequestEventHandler : IQueueEventHandler<CreateReportRequestEvent>
    {

        private readonly IReportRepository _reportRepository;
        private readonly IContactService _contactService;
        private readonly ILogger<CreateReportRequestEventHandler> _logger;
        public CreateReportRequestEventHandler(IReportRepository reportRepository, IContactService contactService, ILogger<CreateReportRequestEventHandler> logger)
        {
            _reportRepository = reportRepository;
            _contactService = contactService;
            _logger = logger;
        }

        public async Task Handle(CreateReportRequestEvent @event)
        {
            _logger.LogInformation("{EventHandler} handle method triggred .Report consumed from queue. ReportId :{ReportId}", nameof(CreateReportRequestEventHandler), @event.Id);

            var report = await _reportRepository.GetReportByIdAsync(@event.Id, CancellationToken.None);
            if (report != null)
            {
                _logger.LogInformation("Report processing started");
                report.ReportStatusType = Domain.Enums.ReportStatusType.Processing;
                await _reportRepository.UpdateReportAsync(report, CancellationToken.None);

                if (report.ReportType == Domain.Enums.ReportType.LocationPersonAndPhoneNumberCountReport)
                {
                    //TODO : persistent build
                    var list = await _contactService.GetPhoneNumberAndPersonCountByLocation(CancellationToken.None);
                    foreach (var row in list)
                    {
                        _logger.LogInformation("LocationName : {LocationName} PhoneNumberCountInLocation:  {PhoneNumberCountInLocation} PersonCountInLocation :{PersonCountInLocation}", row.LocationName,row.PhoneNumberCountInLocation,row.PersonCountInLocation);
                    }
                }

            }
            else
                _logger.LogInformation("Report not found in database");

        }
    }
}
