namespace ContactService.Shared.Dto.ContactInfoDtos
{
    public class SaveContactInfoResponseDto
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string InfoType { get; set; }
        public string InfoContent { get; set; }
    }
}
