using AutoMapper;
using ContactService.Application.Dto.Person;
using ContactService.Core.Domain.Entities;

namespace ContactService.Application.Mapping
{
    public class PersonMapping : Profile
    {

        public PersonMapping()
        {
            CreateMap<Person, CreatePersonRequest>();
        }

    }
}
