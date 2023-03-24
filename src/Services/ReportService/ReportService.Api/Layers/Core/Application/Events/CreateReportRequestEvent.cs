
using BuildingBlocks.EventBus.Absractions;

namespace ReportService.Application.Events
{
    public class CreateReportRequestEvent : IQueueEvent
    {
        public Guid Id { get; set; }
    }
}
