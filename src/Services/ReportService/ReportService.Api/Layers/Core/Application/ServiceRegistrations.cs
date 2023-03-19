using MediatR;
using System.Reflection;

namespace ReportService.Application
{
    public static class ServiceRegistrations
    {
        public static void RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            //services.AddValidatorsFromAssemblyContaining<CreatePersonValidator>(includeInternalTypes: true);
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
