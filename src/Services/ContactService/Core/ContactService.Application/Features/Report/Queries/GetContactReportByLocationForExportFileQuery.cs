using AutoMapper;
using ContactService.Application.Dto.Report;
using ContactService.Application.Interfaces.Repository;
using MediatR;


namespace ContactService.Application.Features.Report.Queries
{
    public class GetContactReportByLocationForExportFileQuery : IRequest<List<ContactReportByLocationDto>>
    {

        public GetContactReportByLocationForExportFileQuery()
        {
            
        }

        public class GetContactReportByLocationForExportFileQueryHandler : IRequestHandler<GetContactReportByLocationForExportFileQuery, List<ContactReportByLocationDto>>
        {
            private readonly IMapper _mapper;
            private readonly IContactInfoRepository _contInfoRepo;

            public GetContactReportByLocationForExportFileQueryHandler(IMapper mapper, IContactInfoRepository contactInfoRepository)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _contInfoRepo = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
            }

            public async Task<List<ContactReportByLocationDto>> Handle(GetContactReportByLocationForExportFileQuery request, CancellationToken cancellationToken)
            {

                var data = await _contInfoRepo.GetContactReportByLocation();

                var result = _mapper.Map<List<ContactReportByLocationDto>>(data);

                return result;
            }
        }
    }
}
