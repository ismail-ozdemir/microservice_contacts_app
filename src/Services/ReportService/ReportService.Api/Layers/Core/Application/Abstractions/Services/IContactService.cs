using ReportService.Application.Dtos;

namespace ReportService.Application.Abstractions.Services
{
    public interface IContactService
    {
        Task<List<PhoneNumberAndPersonCountByLocationDto>> GetPhoneNumberAndPersonCountByLocation(CancellationToken cancellationToken);
    }
}
