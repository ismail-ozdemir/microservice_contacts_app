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
            private readonly IMapper _mapper;
            private readonly ILogger<InsertReportCommandHandler> _logger;
            public InsertReportCommandHandler(IReportRepository reportRepository, IMapper mapper, ILogger<InsertReportCommandHandler> logger)
            {
                _reportRepository = reportRepository ?? throw new ArgumentNullException(nameof(reportRepository));
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _logger = logger;

            }
            public async Task<CreateReportCommandResponseDto> Handle(CreateReportCommand request, CancellationToken cancellationToken)
            {
                var report = _mapper.Map<Report>(request);
                var result = await _reportRepository.InsertReportAsync(report);
                _logger.LogInformation("Report Created");

                return new CreateReportCommandResponseDto(result);
            }
        }
    }


}
