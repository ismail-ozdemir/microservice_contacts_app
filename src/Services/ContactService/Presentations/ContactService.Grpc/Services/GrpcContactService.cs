using ContactService.Application.Features.Report.Queries;
using ContactService.Grpc.Protos;
using Grpc.Core;
using MediatR;


namespace ContactService.Grpc.Services
{
    public class GrpcContactService : ContactProtoService.ContactProtoServiceBase
    {
        private readonly IMediator _mediator;
        public GrpcContactService(IMediator mediator)
        {
            _mediator = mediator;
        }
        public override async Task<ContactReportByLocationResponsePm> GetContactReportByLocation(ContactReportByLocationRequestPm request, ServerCallContext context)
        {

            var query = new GetContactReportByLocationForExportFileQuery();
            var result = await _mediator.Send(query);

            var response = new ContactReportByLocationResponsePm();

            var rpm= result.Select(x=> new ContactReportPm
            {
                LocationName = x.LocationName,
                PersonCountInLocation = x.PersonCountInLocation,
                PhoneNumberCountInLocation = x.PhoneNumberCountInLocation
            });
            response.Data.AddRange(rpm);

            return await Task.FromResult(response);
        }
    }
}
