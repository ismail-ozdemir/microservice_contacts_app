using AutoMapper;
using MediatR;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Application.Dtos;

namespace ReportService.Application.Features.Queries
{
    public class GetReportByIdQuery : IRequest<ReportDto>
    {
        private readonly Guid ReportId;
        public GetReportByIdQuery(Guid reportId)
        {
            ReportId = reportId;
        }

        internal class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, ReportDto>
        {
            private readonly IMapper _mapper;
            private readonly IReportRepository _reportRepository;
            public GetReportByIdQueryHandler(IMapper mapper, IReportRepository reportRepository)
            {
                _mapper = mapper;
                _reportRepository = reportRepository;
            }
            public async Task<ReportDto> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
            {
                var report = await _reportRepository.GetReportByIdAsync(request.ReportId, cancellationToken);
                return _mapper.Map<ReportDto>(report);
            }
        }
    }
}
