using ContactService.Application.Dto.PersonDto;
using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class AddPersonCommand : IRequest<CreatePersonResponse>
    {
        internal readonly CreatePersonRequest data;
        public AddPersonCommand(CreatePersonRequest request)
        {
            data = request ?? throw new ArgumentNullException(nameof(request));
        }
    }

}
