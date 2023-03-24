using AutoMapper;
using MediatR;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Application.Dtos;
using ReportService.Application.Filters;

namespace ReportService.Application.Features.Queries
{
    public class GetReportListQuery : IRequest<List<ReportDto>>
    {
        private readonly ReportFilter _filter;
        public GetReportListQuery(ReportFilter filter)
        {
            _filter = filter;
        }

        internal class GetReportListQueryHandler : IRequestHandler<GetReportListQuery, List<ReportDto>>
        {
            private readonly IMapper _mapper;
            private readonly IReportRepository _reportRepository;
            public GetReportListQueryHandler(IMapper mapper, IReportRepository reportRepository)
            {
                _mapper = mapper;
                _reportRepository = reportRepository;
            }
            public async Task<List<ReportDto>> Handle(GetReportListQuery request, CancellationToken cancellationToken)
            {
                //TODO sayfalama yapılacak
                var reports = await _reportRepository.GetReportsAsync(cancellationToken);
                return _mapper.Map<List<ReportDto>>(reports);
            }
        }
    }
}
