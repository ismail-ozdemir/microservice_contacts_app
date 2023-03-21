
using BuildingBlocks.EventBus.Absractions;

namespace ReportService.Application.Events
{
    public class ReportCreateRequestEvent:IQeueEvent
    {
        public Guid Id { get; set; }
    }
}
