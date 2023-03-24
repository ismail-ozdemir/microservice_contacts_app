using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly.Retry;
using Polly;
using ReportService.Application.Abstractions.Repositories;
using ReportService.Persistence.Concrete;
using ReportService.Persistence.Configuration;
using ReportService.Persistence.Contexts;

namespace ReportService.Persistence
{
    public static class ServiceRegistration
    {
        public static IServiceCollection RegisterPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {

            var settingSection = configuration.GetSection(nameof(DatabaseSettings));
            var conf = settingSection.Get<DatabaseSettings>();
            switch (conf.UseSettings)
            {
                case nameof(PosgreSettings):
                    services.RegisterPostgreServices(settingSection.GetSection(nameof(PosgreSettings)).Get<PosgreSettings>());
                    break;
                case nameof(MangoSettings):
                    services.RegisterMangoServices(settingSection.GetSection(nameof(MangoSettings)).Get<MangoSettings>());
                    break;
                default:
                    services.RegisterPostgreServices(settingSection.GetSection(nameof(PosgreSettings)).Get<PosgreSettings>());
                    break;
            }
            return services;
        }

        public static async Task SetDatabaseMigrations(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                ReportContextRmdb? context = scope.ServiceProvider.GetService<ReportContextRmdb>();
                if (context != null)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ReportContextRmdb>>();
                    var policy = CreatePolicyForMigration(logger, nameof(ReportContextRmdb));
                    await policy.ExecuteAsync(() => ProccessSeedingMigration(context, logger));
                }
            }
        }
        private static AsyncRetryPolicy CreatePolicyForMigration(ILogger<ReportContextRmdb> logger, string prefix, int retries = 5)
        {
            return Policy.Handle<Npgsql.NpgsqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }
        private static async Task ProccessSeedingMigration(ReportContextRmdb context, ILogger<ReportContextRmdb> logger)
        {
            logger.LogInformation("Applying migrations {DbContext}...", nameof(ReportContextRmdb));
            await context.Database.MigrateAsync();
            logger.LogInformation("Applyed migrations {DbContext}...", nameof(ReportContextRmdb));

        }








        private static void RegisterPostgreServices(this IServiceCollection services, PosgreSettings settings)
        {
            services.AddDbContext<ReportContextRmdb>(options =>
            {
                options.UseNpgsql(
                    connectionString: settings.ConnectionString,
                    npgsqlOptionsAction: npgOprions =>
                    {
                        npgOprions.MigrationsAssembly(typeof(ReportContextRmdb).Assembly.GetName().Name);
                        npgOprions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(10), null);
                    }
                    );
            });
            services.AddScoped<IReportRepository, ReportRepositoryRmdb>();
        }
        private static void RegisterMangoServices(this IServiceCollection services, MangoSettings settings)
        {
            services.AddScoped(p => new ReportContextMongo(settings));
            services.AddScoped<IReportRepository, ReportRepositoryMongo>();
        }


    }
}
