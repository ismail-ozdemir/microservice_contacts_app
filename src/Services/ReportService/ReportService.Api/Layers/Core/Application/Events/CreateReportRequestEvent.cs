
using BuildingBlocks.EventBus.Absractions;

namespace ReportService.Application.Events
{
    public class CreateReportRequestEvent:IQeueEvent
    {
        public Guid Id { get; set; }
    }
}
