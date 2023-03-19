using ContactService.Application.Dto.ContactInfo;
using ContactService.Application.Helpers.Pagination;

namespace ContactService.Application.Dto.PersonDto
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;

        public class WithContactInfo : PersonDto
        {

            public PagedResult<ContactInfoDto> ContactInformations { get; set; } = new();
        }

    }
}
