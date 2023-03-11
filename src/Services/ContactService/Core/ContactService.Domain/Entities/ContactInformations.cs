using ContactService.Domain.Common;

namespace ContactService.Core.Domain.Entities
{
    public class ContactInformation: BaseEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public InformationType InformationType { get; set; }
        public string Content { get; set; }

        public User User { get; set; }

    }

}
