
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
            public WithContactInfo()
            {
                ContactInformations = new();
            }
            public PagedResult<ContactInfoDto> ContactInformations { get; set; }
        }

    }

    public class ContactInfoDto
    {
        public Guid InfoId { get; set; }
        public string InfoType { get; set; } = string.Empty;
        public string InfoDetail { get; set; } = string.Empty;
    }
}
