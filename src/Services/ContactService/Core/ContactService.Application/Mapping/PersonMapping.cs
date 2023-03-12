using AutoMapper;
using ContactService.Application.Dto.Person;
using ContactService.Core.Domain.Entities;

namespace ContactService.Application.Mapping
{
    public class PersonMapping : Profile
    {

        public PersonMapping()
        {
            CreateMap<CreatePersonRequest, Person>().ForMember(t => t.Id, opt => opt.Ignore())
                                                    .ForMember(t => t.ContactInformations, opt => opt.Ignore());

            CreateMap<Person, CreatePersonResponse>().ForMember(t => t.PersonId, opt => opt.MapFrom(s => s.Id));
        }

    }
}
