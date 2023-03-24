using BuildingBlocks.EventBus.Absractions;
using MediatR;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Application.Dtos;
using ReportService.Application.Events;
using ReportService.Domain.Entities;
using ReportService.Domain.Enums;

namespace ReportService.Application.Features.Commands
{
    public class CreateReportCommand : IRequest<CreateReportCommandResponseDto>
    {

        public ReportType ReportTypeId { get; set; }


        internal class InsertReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportCommandResponseDto>
        {
            private readonly IReportRepository _reportRepository;

            private readonly ILogger<InsertReportCommandHandler> _logger;
            private readonly IEventBus _eventBus;
            public InsertReportCommandHandler(IReportRepository reportRepository, ILogger<InsertReportCommandHandler> logger, IEventBus eventBus)
            {
                _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
                _logger = logger;
                _eventBus = eventBus;

            }
            public async Task<CreateReportCommandResponseDto> Handle(CreateReportCommand request, CancellationToken cancellationToken)
            {
                var report = new Report
                {
                    ReportType = request.ReportTypeId,
                    ReportStatusType = ReportStatusType.Preparing,
                    RequestDate = DateTime.UtcNow
                };
                Guid reportId = await _reportRepository.InsertReportAsync(report, cancellationToken);
                _logger.LogInformation("Report Created");
                _eventBus.Publish(new CreateReportRequestEvent { Id = reportId });
                _logger.LogInformation("Report published to queue");
                return new CreateReportCommandResponseDto(reportId);
            }
        }
    }


}
