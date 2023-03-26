using ContactService.Shared.Dto.PersonDtos;
using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class CreatePersonCommand : IRequest<CreatePersonResponseDto>
    {
        public CreatePersonRequest request { get; }
        public CreatePersonCommand(CreatePersonRequest request)
        {
            this.request = request;
        }
    }

}
