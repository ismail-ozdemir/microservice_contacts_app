
using BuildingBlocks.EventBus.Base;

namespace BuildingBlocks.EventBus.Absractions
{
    public interface IEventBusSubcriptionManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<TEvent, TEventHandler>() where TEvent : IQueueEvent where TEventHandler : IQueueEventHandler<TEvent>;
        void RemoveSubscription<TEvent, TEventHandler>() where TEvent : IQueueEvent where TEventHandler : IQueueEventHandler<TEvent>;

        bool HasSubcriptionForEvent<TEvent>() where TEvent : IQueueEvent;
        bool HasSubcriptionForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<TEvent>() where TEvent : IQueueEvent;
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<TEvent>() where TEvent : IQueueEvent;
    }
}
