using System.Diagnostics.CodeAnalysis;

namespace ContactService.Shared.Dto.PersonDtos
{
    [ExcludeFromCodeCoverage]
    public class CreatePersonResponseDto
    {
        public Guid PersonId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; }=string.Empty;
        public string Company { get; set; }=string.Empty;
    }
}
