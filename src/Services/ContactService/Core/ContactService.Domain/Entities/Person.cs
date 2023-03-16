using ContactService.Domain.Common;
using System.Diagnostics.CodeAnalysis;

namespace ContactService.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Person : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }

        public ICollection<ContactInformation> ContactInformations { get; set; }

    }

}
