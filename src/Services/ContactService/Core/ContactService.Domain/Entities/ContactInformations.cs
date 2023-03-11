using ContactService.Domain.Common;

namespace ContactService.Core.Domain.Entities
{
    public class ContactInformation : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public InformationType InformationType { get; set; }
        public string Content { get; set; }

        public Person Person { get; set; }

    }

}
