using MediatR;
using Microsoft.AspNetCore.Mvc;
using ContactService.Application.Dto.ContactInfo;
using ContactService.Application.Features.ContactInfoFeatures;
using System.Net;

namespace ContactService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactInfoController : ControllerBase
    {

        private readonly IMediator _mediator;

        public ContactInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(SaveContactInfoResponseDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddContactInfo(InsertContactInfoRequest request)
        {
            InsertContactInfoCommand command = new(request);
            var result = await _mediator.Send(command);
            return new ObjectResult(result) { StatusCode = StatusCodes.Status201Created };
        }


        [HttpDelete]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteContactInfo(DeleteContactInfoCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }


    }
}
