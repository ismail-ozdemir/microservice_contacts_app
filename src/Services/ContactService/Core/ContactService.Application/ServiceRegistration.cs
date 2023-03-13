using ContactService.Application.Behaviours;
using ContactService.Application.Validators.Person;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ContactService.Application
{
    public static class ServiceRegistration
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreatePersonValidator>(includeInternalTypes: true);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }
    }
}
