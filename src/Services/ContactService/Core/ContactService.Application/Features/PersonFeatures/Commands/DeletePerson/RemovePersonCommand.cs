using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class RemovePersonCommand : IRequest<string>
    {
        public Guid Id { get; }
        public RemovePersonCommand(Guid Id)
        {
            this.Id = Id;
        }
    }
}
