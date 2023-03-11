using ContactService.Application.Dto.Person;
using ContactService.Application.Validators.Person;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Application
{
    public static class ServiceRegistration
    {
        public static void RegisterApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<CreatePersonValidator>(includeInternalTypes:true);   
        }
    }
}
