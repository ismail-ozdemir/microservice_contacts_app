using ContactService.Application.Validators.Person;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ContactService.Application
{
    public static class ServiceRegistration
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreatePersonValidator>(includeInternalTypes:true);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }
    }
}
