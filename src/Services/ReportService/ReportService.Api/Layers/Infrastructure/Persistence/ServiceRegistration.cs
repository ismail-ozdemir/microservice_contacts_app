using Microsoft.EntityFrameworkCore;
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
            switch (settingSection["DatabaseType"])
            {
                case "Postgre":
                    services.RegisterPostgreServices(settingSection.Get<PosgreSettings>());
                    break;
                case "Mongo":
                    services.RegisterMangoServices(settingSection.Get<MangoSettings>());
                    break;
                default:
                    services.RegisterPostgreServices(settingSection.Get<PosgreSettings>());
                    break;
            }
            return services;
        }

        public static void SetDatabaseMigrations(this IApplicationBuilder app)
        {

            using (var scope = app.ApplicationServices.CreateScope())
            {
                ReportContextRmdb? context = scope.ServiceProvider.GetService<ReportContextRmdb>();
                if (context != null)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<ReportContextRmdb>>();
                    logger.LogInformation("Applying migrations {DbContext}...", nameof(ReportContextRmdb));
                    context.Database.Migrate();
                    logger.LogInformation("Applyed migrations {DbContext}...", nameof(ReportContextRmdb));
                }
            }
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
