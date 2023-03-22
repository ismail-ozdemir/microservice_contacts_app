using ContactService.Grpc.Protos;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using ReportService.Application.Abstractions.Services;
using ReportService.Application.Dtos;
using ReportService.Infrastructure.Configurations;

namespace ReportService.Infrastructure.Services
{
    public class ContactServiceGrpc : IContactService
    {

        private readonly ContactProtoService.ContactProtoServiceClient _client;
        public ContactServiceGrpc(IOptions<ServiceEndpointSettings> options)
        {
            var channel = GrpcChannel.ForAddress(options.Value.ContactService_Grpc);
            _client = new ContactProtoService.ContactProtoServiceClient(channel);
        }

        public async Task<List<PhoneNumberAndPersonCountByLocationDto>> GetPhoneNumberAndPersonCountByLocation(string locationName, CancellationToken cancellationToken)
        {

            var response = await _client.GetContactReportByLocationAsync(
                request: new ContactReportByLocationRequestPm { LocationName = locationName },
                cancellationToken: cancellationToken
                );

            return response.Data.Select(x => new PhoneNumberAndPersonCountByLocationDto(x.LocationName, x.PersonCountInLocation, x.PhoneNumberCountInLocation)).ToList();

        }
    }
}
