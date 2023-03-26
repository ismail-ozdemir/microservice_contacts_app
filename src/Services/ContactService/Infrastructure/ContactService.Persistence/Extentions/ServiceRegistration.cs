using ContactService.Application.Interfaces.Repository;
using ContactService.Persistence.Concrete.Repositories;

using ContactService.Persistence.Configurations;
using ContactService.Persistence.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace ContactService.Persistence.Extentions
{
    public static class ServiceRegistration
    {
        public static void RegisterPersistence(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder? hcBuilder = null)
        {
            DatabaseSettings? dbSetting = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
            if (dbSetting == null)
                throw new ArgumentNullException(nameof(DatabaseSettings));

            services.AddDbContext<ContactsContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: dbSetting.ConnectionString,
                    npgsqlOptionsAction: npgOprions =>
                    {
                        npgOprions.MigrationsAssembly(typeof(ContactsContext).Assembly.GetName().Name);
                        npgOprions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(10), null);
                    }
                    );
            });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IContactInfoRepository, ContactInfoRepository>();

            if (hcBuilder != null)
            {
                hcBuilder.AddNpgSql(
                    npgsqlConnectionString: dbSetting.ConnectionString,
                    healthQuery: "SELECT 1",
                    name: "Postgre Check",
                    failureStatus: HealthStatus.Unhealthy,
                    tags: new string[] { "db", "sql", "postgre" }
                    );
            }
        }







        public static void SetDatabaseMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ContactsContext>>();
                logger.LogInformation("Applying migrations {DbContext}...", nameof(ContactsContext));

                ContactsContext context = scope.ServiceProvider.GetRequiredService<ContactsContext>();
                context.Database.Migrate();

                logger.LogInformation("Applyed migrations {DbContext}...", nameof(ContactsContext));
            }
        }
        public static async Task UseSeedDataAsync(this IApplicationBuilder builder)
        {

            using var scope = builder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ContactsContext>();
            var loger = scope.ServiceProvider.GetRequiredService<ILogger<ContactContextSeed>>();

            ContactContextSeed seedContext = new();
            await seedContext.SeedAsync(context, loger);

        }
    }
}
