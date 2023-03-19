﻿using MediatR;


namespace ContactService.Application.Features.PersonFeatures.Commands
{
    public class RemovePersonCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }
}
