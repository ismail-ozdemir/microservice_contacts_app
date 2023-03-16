

namespace ContactService.Application.ViewModels
{
    public class ContactInfoWm
    {
        public Guid InfoId { get; set; }
        public string InfoType { get; set; } = string.Empty;
        public string InfoContent { get; set; } = string.Empty;

        public class WithPersonId : ContactInfoWm
        {
            public Guid PersonId { get; set; }
        }
    }
}
