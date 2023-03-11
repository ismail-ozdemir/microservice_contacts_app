using ContactService.Application.Dto.Person;
using ContactService.Application.Interfaces.Services;
using ContactService.Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace ContactService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService personService;

        public PersonController(IPersonService personService)
        {
            this.personService = personService;
        }


        [HttpPost]
        [ProducesResponseType(typeof(ServiceResponse<CreatePersonResponse>), 200)]
        public async Task<IActionResult> AddAsync(CreatePersonRequest request)
        {
            var response = await personService.AddAsync(request);
            return Ok(response);
        }
    }
}
