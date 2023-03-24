using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReportService.Application.Features.Commands;
using ReportService.Application.Features.Queries;
using ReportService.Application.Filters;
using System;

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
            var result = await _mediator.Send(new GetReportListQuery(filter), cancellationToken);
            return Ok(result);
        }
        [HttpGet("DownloadReport")]
        public async Task<IActionResult> DownloadReport([FromQuery] Guid ReportId, CancellationToken cancellationToken)
        {

            var result = await _mediator.Send(new GetReportByIdQuery(ReportId), cancellationToken);
            
            FileInfo file = new FileInfo(result.FilePath);
            if (file.Exists)
            {
                FileStream fileStream = new FileStream(file.FullName, FileMode.Open);
                fileStream.Position = 0;
                var contentType = "application/octet-stream";
                return File(fileStream, contentType, file.Name);
            }
            else
                return NotFound();
            
        }




    }
}
