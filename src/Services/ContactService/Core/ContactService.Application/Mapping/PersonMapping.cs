using AutoMapper;
using Common.Shared.Wrappers;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.ViewModels;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Domain.Entities;
using ContactService.Shared.Dto.ContactInfoDtos;
using ContactService.Shared.Dto.PersonDtos;

namespace ContactService.Application.Mapping
{
    public class PersonMapping : Profile
    {

        public PersonMapping()
        {
            CreateMap<CreatePersonRequest, Person>().ForMember(t => t.Id, opt => opt.Ignore())
                                                    .ForMember(t => t.ContactInformations, opt => opt.Ignore());

            CreateMap<Person, CreatePersonResponseDto>().ForMember(t => t.PersonId, opt => opt.MapFrom(s => s.Id));

            CreateMap<PersonWm, PersonResponse>();
            CreateMap<PagedResult<PersonWm>, PagedResult<PersonResponse>>();


            CreateMap<ContactInfoWm, ContactInfoResponseDto>().ForMember(t => t.InfoDetail, opt => opt.MapFrom(s => s.InfoContent));
            CreateMap<PagedResult<ContactInfoWm>, PagedResult<ContactInfoResponseDto>>();

        }

    }
}
