using AutoMapper;
using ContactService.Application.Dto.Person;
using ContactService.Core.Domain.Entities;

namespace ContactService.Application.Mapping
{
    public class PersonelMapping : Profile
    {

        public PersonelMapping()
        {
            CreateMap<Person, CreatePersonRequest>();
        }

    }
}
