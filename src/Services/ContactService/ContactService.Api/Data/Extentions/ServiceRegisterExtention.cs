using ContactService.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Runtime;


namespace ContactService.Api.Data.Extentions
{
    public static class ServiceRegisterExtention
    {

        public static IServiceCollection AddDataContext(this IServiceCollection services, IConfiguration configuration)
        {

            //TODO: farklı veritabanları desteğide eklenebilir.
            DatabaseSettings dbSetting = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();

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
            return services;
        }
        public static void SetDatabaseMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                ContactsContext context = scope.ServiceProvider.GetRequiredService<ContactsContext>();
                context.Database.Migrate();
            }
        }


    }
}
