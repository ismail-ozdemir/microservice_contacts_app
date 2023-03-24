namespace BuildingBlocks.EventBus.Absractions
{
    public interface IEventBus
    {
        public void Publish(IQueueEvent @event);
        void Subscribe<TQueueEvent, TQueueEventHandler>() where TQueueEvent : IQueueEvent where TQueueEventHandler : IQueueEventHandler<TQueueEvent>;
    }
}
