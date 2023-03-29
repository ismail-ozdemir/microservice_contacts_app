namespace EventBus.Base.Abstractions
{
    public interface IEventBus
    {
        public void Publish(IEvent @event);
        void Subscribe<TQueueEvent, TQueueEventHandler>() where TQueueEvent : IEvent where TQueueEventHandler : IEventHandler<TQueueEvent>;
    }
}
