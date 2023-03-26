using EventBus.Abstractions;
using System.Xml.Linq;

namespace EventBus.Base.SubManagers
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubcriptionManager
    {

        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string>? OnEventRemoved;

        public InMemoryEventBusSubscriptionManager()
        {
            _handlers = new();
            _eventTypes = new();
        }

        /// <summary>
        /// eventhandler listesinin boş olup olmadıpı bilgisi
        /// </summary>
        public bool IsEmpty => !_handlers.Keys.Any();

        /// <summary>
        /// Event için name(key) döner
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <returns></returns>
        public string GetEventKey<TEvent>() where TEvent : IEvent => typeof(TEvent).Name;
        /// <summary>
        /// Event'ın Type ını döner
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public Type GetEventTypeByName(string eventName) => _eventTypes.First(e => e.Name == eventName);

        /// <summary>
        /// Event ve Handler'ı register eder
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <typeparam name="EventHandler"></typeparam>
        public void AddSubscription<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>
        {
            string eventName = GetEventKey<TEvent>();
            AddSubscription(eventName, typeof(TEventHandler));

            if (!_eventTypes.Contains(typeof(TEvent)))
                _eventTypes.Add(typeof(TEvent));

        }

        /// <summary>
        /// Event ve Handler'ı siler
        /// </summary>
        /// <typeparam name="Event"></typeparam>
        /// <typeparam name="EventHandler"></typeparam>
        public void RemoveSubscription<Event, EventHandler>() where Event : IEvent where EventHandler : IEventHandler<Event>
        {

            SubscriptionInfo? handlerToRemove = FindSubsCriptionToRemove<Event, EventHandler>();
            string eventName = GetEventKey<Event>();
            RemoveHandler(eventName, handlerToRemove);


        }

        /// <summary>
        /// eventhandler listesini temizler
        /// </summary>
        public void Clear() => _handlers.Clear();

        /// <summary>
        /// Event'ın Handler'larını döner
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IEvent
        {
            string eventName = GetEventKey<T>();
            return GetHandlersForEvent(eventName);
        }

        /// <summary>
        /// Event'ın Handler'larını döner
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];


        /// <summary>
        /// Event için Handler var mı ?
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool HasSubcriptionForEvent<T>() where T : IEvent
        {
            string eventName = GetEventKey<T>();
            return HasSubcriptionForEvent(eventName);
        }

        /// <summary>
        /// Event için Handler var mı ?
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool HasSubcriptionForEvent(string eventName) => _handlers.ContainsKey(eventName);


        #region private alanlar

        private void AddSubscription(string eventName, Type handlerType)
        {
            if (!HasSubcriptionForEvent(eventName))
                _handlers.Add(eventName, new());

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
                throw new ArgumentException($"Handler Type {handlerType.Name} already registerd for '{eventName}'", nameof(handlerType));

            _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
        }
        private SubscriptionInfo? FindSubsCriptionToRemove<Event, EventHandler>() where Event : IEvent where EventHandler : IEventHandler<Event>
        {
            string eventName = GetEventKey<Event>();
            return FindSubsCriptionToRemove(eventName, typeof(EventHandler));
        }
        private SubscriptionInfo? FindSubsCriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubcriptionForEvent(eventName))
                return null;
            return _handlers[eventName].FirstOrDefault(s => s.HandlerType == handlerType);
        }

        private void RemoveHandler(string eventName, SubscriptionInfo? handler)
        {
            if (handler is not null)
            {
                _handlers[eventName].Remove(handler);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    Type? eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType is not null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }
            }
        }
        private void RaiseOnEventRemoved(string eventName)
        {
            OnEventRemoved?.Invoke(this, eventName);
        }
        #endregion

    }

}
