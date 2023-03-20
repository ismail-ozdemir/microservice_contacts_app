using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Features.Commands;
using ReportService.Application.Filters;

namespace ReportService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IMediator _mediator;

        public ReportController(ILogger<ReportController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportCommand createReportRequest, CancellationToken cancellicationToken)
        {
            var result = await _mediator.Send(createReportRequest, cancellicationToken);
            return Ok(result);
        }

        [HttpGet("GetReports")]
        public async Task<IActionResult> GetReports([FromQuery] ReportFilter filter, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(filter, cancellationToken);
            return Ok(result);
        }

    }
}
