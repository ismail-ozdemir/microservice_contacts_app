using BuildingBlocks.EventBus.Absractions;
using BuildingBlocks.EventBus.RabbitMQ;
using ReportService.Api.Configurations;
using ReportService.Application.Events;

namespace ReportService.Infrastructure
{

    public static class ServiceRegisterExtentions
    {
        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var conf = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
            if (conf == null)
                throw new ArgumentNullException($"{nameof(RabbitMQSettings)} not defined appsettings");
            else
            {
                services.AddSingleton<IEventBus>(provider => new EventBusRabbitMQ(provider, new()
                {
                    EventBusConnectionString = conf.ConnectionString,
                    SubscriberClientAppName = "ReportService"
                }));
                services.AddTransient<CreateReportRequestEventHandler>();
            }

        }
        public static void UseEventBus(this WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();
            eventBus.Subscribe<CreateReportRequestEvent, CreateReportRequestEventHandler>();
        }




    }
}
