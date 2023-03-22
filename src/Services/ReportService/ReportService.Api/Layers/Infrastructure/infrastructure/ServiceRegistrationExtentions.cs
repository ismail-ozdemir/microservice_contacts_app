using Microsoft.Extensions.Configuration;
using ReportService.Application.Abstractions.Services;
using ReportService.Infrastructure.Configurations;
using ReportService.Infrastructure.Services;

namespace ReportService.Infrastructure
{
    public static class ServiceRegistrationExtentions
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceEndpointSettings>(configuration.GetSection(nameof(ServiceEndpointSettings)));
            services.AddSingleton<IContactService, ContactServiceGrpc>();



            return services;
        }
    }
}
