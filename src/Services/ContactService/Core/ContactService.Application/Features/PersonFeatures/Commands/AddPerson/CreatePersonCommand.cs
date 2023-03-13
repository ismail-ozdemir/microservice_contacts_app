using ContactService.Application.Dto.PersonDto;
using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class CreatePersonCommand : IRequest<CreatePersonResponse>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }

}
