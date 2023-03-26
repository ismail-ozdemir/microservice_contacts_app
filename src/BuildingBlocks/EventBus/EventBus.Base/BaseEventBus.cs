using EventBus.Base.SubManagers;
using EventBus.Abstractions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus.Base
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
        public abstract void Publish(IEvent @event);
        public abstract void Subscribe<TEvent, TEventHandler>() where TEvent : IEvent where TEventHandler : IEventHandler<TEvent>;
      

        public async Task<bool> ProcessEvent(string eventName, string Message, Action<bool> OnComplated)
        {
            
            bool processed = false;
            if (SubsManager.HasSubcriptionForEvent(eventName))
            {
                IEnumerable<SubscriptionInfo> subscriptions = SubsManager.GetHandlersForEvent(eventName);
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    foreach (SubscriptionInfo subscription in subscriptions)
                    {
                        var handler = scope.ServiceProvider.GetRequiredService(subscription.HandlerType);
                        if (handler == null)
                        {
                            _logger.LogCritical("{EventHandler} not registered", subscription.HandlerType.FullName);
                            continue;
                        }
                        Type eventType = SubsManager.GetEventTypeByName(eventName);
                        object Event = JsonConvert.DeserializeObject(Message, eventType)!;
                        Type concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        await (Task)concreteType!.GetMethod(nameof(IEventHandler<IEvent>.Handle))!.Invoke(handler, new object[] { Event })!;

                    }
                }
                processed = true;
            }
            OnComplated(processed);
            return processed;
        }

    }
}
