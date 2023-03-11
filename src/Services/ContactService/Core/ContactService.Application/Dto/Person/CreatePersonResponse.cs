using ContactService.Application.Wrappers;

namespace ContactService.Application.Dto.Person
{
    public class CreatePersonResponse
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }
}
