using AutoMapper;
using MediatR;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Application.Dtos;
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
            public InsertReportCommandHandler(IReportRepository reportRepository,ILogger<InsertReportCommandHandler> logger)
            {
                _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
                _logger=logger ?? throw new ArgumentNullException(nameof(logger));
            

            }
            public async Task<CreateReportCommandResponseDto> Handle(CreateReportCommand request, CancellationToken cancellationToken)
            {
                var report = new Report
                {
                    ReportType=request.ReportTypeId,
                    ReportStatusType=ReportStatusType.Preparing,
                    RequestDate=DateTime.UtcNow 
                };
                var result = await _reportRepository.InsertReportAsync(report, cancellationToken);
                _logger.LogInformation("Report Created");

                return new CreateReportCommandResponseDto(result);
            }
        }
    }


}
