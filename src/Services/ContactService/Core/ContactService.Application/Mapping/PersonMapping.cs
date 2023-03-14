using AutoMapper;
using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.ViewModels.PersonVms;
using ContactService.Domain.Entities;

namespace ContactService.Application.Mapping
{
    public class PersonMapping : Profile
    {

        public PersonMapping()
        {
            CreateMap<CreatePersonCommand, Person>().ForMember(t => t.Id, opt => opt.Ignore())
                                                    .ForMember(t => t.ContactInformations, opt => opt.Ignore());

            CreateMap<Person, CreatePersonResponseDto>().ForMember(t => t.PersonId, opt => opt.MapFrom(s => s.Id));

            CreateMap<PersonWm, PersonDto>();
        }

    }
}
