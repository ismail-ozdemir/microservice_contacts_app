namespace ContactService.Api.Data.Models
{
    public class ContactInformation
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public InformationType InformationType { get; set; }
        public string Content { get; set; }

        public User User { get; set; }

    }

}
