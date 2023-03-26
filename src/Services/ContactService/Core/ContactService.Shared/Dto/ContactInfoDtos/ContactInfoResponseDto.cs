using System.Diagnostics.CodeAnalysis;

namespace ContactService.Shared.Dto.ContactInfoDtos
{
    [ExcludeFromCodeCoverage]
    public class ContactInfoResponseDto
    {
        public Guid InfoId { get; set; }
        public string InfoType { get; set; } = string.Empty;
        public string InfoDetail { get; set; } = string.Empty;
    }
}
