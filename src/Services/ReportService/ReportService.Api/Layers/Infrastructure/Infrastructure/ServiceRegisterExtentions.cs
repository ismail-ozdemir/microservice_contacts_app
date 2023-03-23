using BuildingBlocks.EventBus.Absractions;
using BuildingBlocks.EventBus.RabbitMQ;
using ReportService.Api.Configurations;
using ReportService.Application.Abstractions.Services;
using ReportService.Application.Events;
using ReportService.Infrastructure.Configurations;
using ReportService.Infrastructure.Services;

namespace ReportService.Infrastructure
{

    public static class ServiceRegisterExtentions
    {

        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceEndpointSettings>(configuration.GetSection(nameof(ServiceEndpointSettings)));
            services.AddTransient<IContactService, ContactServiceGrpc>();
            services.AddSingleton<IFileService,FileService>();

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

            return services;
        }
        public static void UseEventBus(this WebApplication app)
        {
            var eventBus = app.Services.GetRequiredService<IEventBus>();
            eventBus.Subscribe<CreateReportRequestEvent, CreateReportRequestEventHandler>();
        }



    }
}
