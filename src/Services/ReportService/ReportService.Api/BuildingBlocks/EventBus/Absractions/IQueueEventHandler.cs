namespace BuildingBlocks.EventBus.Absractions
{
    public interface IQueueEventHandler
    {
    }

    public interface IQueueEventHandler<in TEvent> : IQueueEventHandler where TEvent : IQueueEvent
    {
        Task Handle(TEvent @event);
    }
}
