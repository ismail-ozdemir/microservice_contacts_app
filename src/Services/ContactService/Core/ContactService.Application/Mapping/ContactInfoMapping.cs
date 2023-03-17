using AutoMapper;
using ContactService.Application.Dto.ContactInfo;
using ContactService.Domain.Entities;

namespace ContactService.Application.Mapping
{
    public class ContactInfoMapping : Profile
    {
        public ContactInfoMapping()
        {
            CreateMap<InsertContactInfoRequest, ContactInformation>()
                        .ForMember(t => t.Id, opt => opt.Ignore())
                        .ForMember(t => t.PersonId, opt => opt.MapFrom(s => s.PersonId))
                        .ForMember(t => t.InformationType, opt => opt.MapFrom(s => s.InfoType))
                        .ForMember(t => t.Content, opt => opt.MapFrom(s => s.InfoContent))
                        .ForMember(t => t.Person, opt => opt.Ignore());


            CreateMap<ContactInformation, SaveContactInfoResponseDto>()
                //.ConstructUsing((source) => new SaveContactInfoResponseDto(source.Id, source.PersonId, source.InformationType.ToString(), source.Content))
                        .ForMember(t => t.InfoType, opt => opt.MapFrom(s => s.InformationType))
                        .ForMember(t => t.InfoContent, opt => opt.MapFrom(s => s.Content));

        }

    }
}
