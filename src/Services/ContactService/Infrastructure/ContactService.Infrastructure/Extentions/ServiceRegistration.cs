using ContactService.Application.Interfaces.Services;
using ContactService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Persistence.Extentions
{
    public static class ServiceRegistration
    {
        public static void RegisterInfrastructer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IReportService, ReportService>();

        }
    }
}
