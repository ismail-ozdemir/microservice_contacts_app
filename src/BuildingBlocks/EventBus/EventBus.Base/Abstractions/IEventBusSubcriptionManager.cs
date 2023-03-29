namespace EventBus.Base.Abstractions
{
    public interface IEventBusSubcriptionManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;
        void RemoveSubscription<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;

        bool HasSubcriptionForEvent<TEvent>() where TEvent : IEvent;
        bool HasSubcriptionForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : IEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<TEvent>() where TEvent : IEvent;
    }
}
