using EventBus.Base.Abstractions;
using EventBus.Config;
using Microsoft.Extensions.DependencyInjection;


namespace EventBus.Base
{
    public static class ServiceRegistrations
    {
        public static void RegisterEventBus(this IServiceCollection services, Func<EventBusConfig, IEventBus> action)
        {
            EventBusConfig conf = new EventBusConfig();
            IEventBus eventBus = action(conf);
            services.AddSingleton(eventBus);
        }
        

    }
}
