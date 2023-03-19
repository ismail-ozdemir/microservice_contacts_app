using AutoMapper;
using ContactService.Application.Dto.Report;
using ContactService.Application.Interfaces.Repository;
using MediatR;


namespace ContactService.Application.Features.Report.Queries
{
    public class GetContactReportByLocationForExportFileQuery : IRequest<ContactReportByLocationDto>
    {
        public string LocationName { get; set; }

        public GetContactReportByLocationForExportFileQuery(string locationName)
        {
            LocationName = locationName;
        }

        public class GetContactReportByLocationForExportFileQueryHandler : IRequestHandler<GetContactReportByLocationForExportFileQuery, ContactReportByLocationDto>
        {
            private readonly IMapper _mapper;
            private readonly IContactInfoRepository _contInfoRepo;

            public GetContactReportByLocationForExportFileQueryHandler(IMapper mapper, IContactInfoRepository contactInfoRepository)
            {
                _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
                _contInfoRepo = contactInfoRepository ?? throw new ArgumentNullException(nameof(contactInfoRepository));
            }

            public async Task<ContactReportByLocationDto> Handle(GetContactReportByLocationForExportFileQuery request, CancellationToken cancellationToken)
            {

                var data = await _contInfoRepo.GetContactReportByLocation(request.LocationName);

                var result = _mapper.Map<ContactReportByLocationDto>(data);

                return result;
            }
        }
    }
}
