
namespace ContactService.Application.Dto.ContactInfo
{
    public class SaveContactInfoResponseDto
    {
        //public SaveContactInfoResponseDto(Guid id, Guid personId, string infoType, string infoContent)
        //{
        //    Id = id;
        //    PersonId = personId;
        //    InfoType = infoType;
        //    InfoContent = infoContent;
        //}

        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string InfoType { get; set; }
        public string InfoContent { get; set; }
    }
}
