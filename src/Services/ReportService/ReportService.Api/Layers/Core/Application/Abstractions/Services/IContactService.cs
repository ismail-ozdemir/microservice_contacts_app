using ReportService.Application.Dtos;

namespace ReportService.Application.Abstractions.Services
{
    public interface IContactService
    {
        Task<List<PhoneNumberAndPersonCountByLocationDto>> GetPhoneNumberAndPersonCountByLocation(string locationName, CancellationToken cancellationToken);
    }
}
