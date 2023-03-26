namespace ContactService.Shared.Dto.ContactInfoDtos
{
    public class InsertContactInfoRequest
    {
        public Guid PersonId { get; set; }
        public string InfoType { get; set; } = string.Empty;
        public string InfoContent { get; set; } = string.Empty;
    }
}
