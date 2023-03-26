using Common.Shared.Api.Proxy;
using Common.Shared.Api.Wrappers;
using Common.Shared.Wrappers;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Features.PersonFeatures.Queries;
using ContactService.Shared.Dto.PersonDtos;
using ContactService.Shared.Filters;
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

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PagedResult<PersonResponse>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersons([FromQuery] PersonFilter filter)
        {
            var query = new GetPersonListQuery(filter);
            var result = await CallServiceProxy.CallServiceAsync(() => _mediator.Send(query, CancellationToken.None));
            return Ok(result);
        }

        [HttpGet("GetContactInfo")]
        [ProducesResponseType(typeof(ApiResponse<PersonResponse.WithContactInfo>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonContactInfo([FromQuery] PersonFilter.ById filter)
        {
            var query = new GetPersonContactInfoList(filter);
            var result = await CallServiceProxy.CallServiceAsync(() => _mediator.Send(query, CancellationToken.None));
            return Ok(result);
        }



        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<CreatePersonResponseDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddAsync(CreatePersonRequest request)
        {
            var entity = await CallServiceProxy.CallServiceAsync(() => _mediator.Send(new CreatePersonCommand(request)));
            return new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
        }


        [HttpDelete]
        [ProducesResponseType(typeof(ApiResponse<string>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            var result = await CallServiceProxy.CallServiceAsync(() => _mediator.Send(new RemovePersonCommand(id)));
            return Ok(result);
        }
    }
}
