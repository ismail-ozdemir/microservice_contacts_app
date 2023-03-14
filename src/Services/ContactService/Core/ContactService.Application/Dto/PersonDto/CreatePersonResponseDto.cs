

namespace ContactService.Application.Dto.PersonDto
{
    public class CreatePersonResponseDto
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }
}
