namespace ContactService.Application.Dto.ContactInfo
{
    public class ContactInfoDto
    {
        public Guid InfoId { get; set; }
        public string InfoType { get; set; } = string.Empty;
        public string InfoDetail { get; set; } = string.Empty;
    }
}
