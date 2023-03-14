using ContactService.Application.Dto.PersonDto;
using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    //TODO : command ve requestler ayrılmalı mı ?
    public class CreatePersonCommand : IRequest<CreatePersonResponseDto>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Company { get; set; }
    }

}
