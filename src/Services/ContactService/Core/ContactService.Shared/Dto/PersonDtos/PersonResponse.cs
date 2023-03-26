using Common.Shared.Wrappers;
using ContactService.Shared.Dto.ContactInfoDtos;
using System.Diagnostics.CodeAnalysis;

namespace ContactService.Shared.Dto.PersonDtos
{
    [ExcludeFromCodeCoverage]
    public class PersonResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        public class WithContactInfo : PersonResponse
        {
            public PagedResult<ContactInfoResponseDto> ContactInformations { get; set; } = new();
        }

    }
}
