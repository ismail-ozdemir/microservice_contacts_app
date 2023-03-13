using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace ContactService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PersonController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync(CreatePersonRequest request)
        {
            var command = new AddPersonCommand(request);
            var person = await _mediator.Send(command);

            return Ok(person);
        }
    }
}
