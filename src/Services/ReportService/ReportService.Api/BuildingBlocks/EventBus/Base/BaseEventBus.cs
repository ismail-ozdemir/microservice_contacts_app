using BuildingBlocks.EventBus.Absractions;
using BuildingBlocks.EventBus.Base.SubManagers;
using Newtonsoft.Json;

namespace BuildingBlocks.EventBus.Base
{
    public abstract class BaseEventBus : IEventBus
    {

        protected readonly IServiceProvider _serviceProvider;
        public readonly IEventBusSubcriptionManager SubsManager;
        public readonly ILogger<BaseEventBus> _logger;
        protected EventBusConfig _config { get; set; }
        protected BaseEventBus(IServiceProvider serviceProvider, EventBusConfig config)
        {
            _config = config;
            _serviceProvider = serviceProvider;
            SubsManager = new InMemoryEventBusSubscriptionManager();
            _logger = serviceProvider.GetRequiredService<ILogger<BaseEventBus>>();
        }
        public abstract void Publish(IQueueEvent @event);
        public abstract void Subscribe<TQueueEvent, TQueueEventHandler>() where TQueueEvent : IQueueEvent where TQueueEventHandler : IQueueEventHandler<TQueueEvent>;
      
        /// <summary>
        /// Gelen mesaj için tanımlanan eventhandler'ları çağırır
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="Message"></param>
        /// <returns>
        /// True : eventhandler'a erişilmiş ve handle işlemi tetiklenmiştir.
        /// False : eventandler'a erişilememiştir.
        /// </returns>
        public async Task<bool> ProcessEvent(string eventName, string Message, Action<bool> OnComplated)
        {
            
            bool processed = false;
            if (SubsManager.HasSubcriptionForEvent(eventName))
            {
                IEnumerable<SubscriptionInfo> subscriptions = SubsManager.GetHandlersForEvent(eventName);
                using (var scope = _serviceProvider.CreateScope())
                {
                    foreach (SubscriptionInfo subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetRequiredService(subscription.HandlerType);
                        if (handler == null)
                        {
                            _logger.LogCritical("{EventHandler} not registered", subscription.HandlerType.FullName);
                            continue;
                        }
                        var eventType = SubsManager.GetEventTypeByName(eventName);
                        var QueueEvent = JsonConvert.DeserializeObject(Message, eventType)!;

                        var concreteType = typeof(IQueueEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType!.GetMethod("Handle")!.Invoke(handler, new object[] { QueueEvent })!;

                    }
                }
                processed = true;

            }
            OnComplated(processed);
            return processed;
        }

    }
}
