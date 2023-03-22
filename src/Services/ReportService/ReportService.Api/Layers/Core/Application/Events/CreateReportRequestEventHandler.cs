
using BuildingBlocks.EventBus.Absractions;
using ReportService.Application.Abstractions.Repositories;

namespace ReportService.Application.Events
{
    public class CreateReportRequestEventHandler : IQueueEventHandler<CreateReportRequestEvent>
    {
        private readonly IReportRepository _reportRepository;

        public Task Handle(CreateReportRequestEvent @event)
        {

            throw new NotImplementedException();
        }
    }
}
