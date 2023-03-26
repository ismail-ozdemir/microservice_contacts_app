

namespace ContactService.Shared.Dto.PersonDtos
{
    public class CreatePersonRequest
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
    }
}
