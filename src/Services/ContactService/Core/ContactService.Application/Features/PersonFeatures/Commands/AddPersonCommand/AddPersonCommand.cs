using ContactService.Application.Dto.PersonDto;
using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class AddPersonCommand : IRequest<CreatePersonResponse>
    {
        private readonly CreatePersonRequest _request;
        public AddPersonCommand(CreatePersonRequest request)
        {
            _request = request ?? throw new ArgumentNullException(nameof(request));
        }
    }


}
