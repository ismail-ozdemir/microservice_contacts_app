using ContactService.Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;
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
                Log.Information("Applying migrations {DbContext}...", nameof(ContactsContext));

                ContactsContext context = scope.ServiceProvider.GetRequiredService<ContactsContext>();
                context.Database.Migrate();

                Log.Information("Applyed migrations {DbContext}...", nameof(ContactsContext));
            }
        }


        public static async void UseSeedData(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ContactsContext>();
            var loger = scope.ServiceProvider.GetRequiredService<ILogger<ContactContextSeed>>();

            ContactContextSeed seedContext = new();
            await seedContext.SeedAsync(context, loger);
        }


    }
}
