using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        // TODO :  badrequest dönüşleri
        [HttpPost]
        [ProducesResponseType(typeof(CreatePersonResponse), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddAsync(CreatePersonCommand command)
        {
            var entity = await _mediator.Send(command);
            return new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
        }


        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveAsync(RemovePersonCommand command)
        {
            string result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
