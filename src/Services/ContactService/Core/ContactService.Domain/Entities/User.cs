using ContactService.Domain.Common;

namespace ContactService.Core.Domain.Entities
{
    public class User: BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public ICollection<ContactInformation> ContactInformations { get; set; }

    }

}
