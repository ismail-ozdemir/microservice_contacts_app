using EventBus.Base.Abstractions;
using EventBus.Config;

namespace EventBus.RabbitMQ.Configurations
{
    public static class EventBusConfigExtentions
    {
        public static IEventBus UseRabbitMQ(this EventBusConfig configuration, RabbitMqConfig conf, IServiceProvider provider)
        {
            return new EventBusRabbitMQ(provider, conf);
        }
    }
}
