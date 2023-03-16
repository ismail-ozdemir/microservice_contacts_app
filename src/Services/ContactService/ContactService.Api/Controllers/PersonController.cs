using ContactService.Application.Dto.PersonDto;
using ContactService.Application.Features.PersonFeatures.Commands;
using ContactService.Application.Features.PersonFeatures.Queries;
using ContactService.Application.Filters.PersonFilters;
using ContactService.Application.Helpers.Pagination;
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
        [ProducesResponseType(typeof(PagedResult<PersonDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersons([FromQuery] PersonFilter filter)
        {
            var query = new GetPersonListQuery(filter);
            var result = await _mediator.Send(query, CancellationToken.None);
            return Ok(result);
        }

        [HttpGet("GetContactInfo")]
        [ProducesResponseType(typeof(PersonDto.WithContactInfo), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonContactInfo([FromQuery] PersonFilter.ById filter)
        {
            var query = new GetPersonContactInfoList(filter);
            var result = await _mediator.Send(query, CancellationToken.None);
            return Ok(result);
        }



        // TODO :  badrequest dönüşleri
        [HttpPost]
        [ProducesResponseType(typeof(CreatePersonResponseDto), (int)HttpStatusCode.Created)]
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
