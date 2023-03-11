using Serilog;
using ContactService.Persistence.Extentions;
using ContactService.Application;

IConfiguration configuration = GetConfiguration();
Log.Logger = CreateSerilogLogger(configuration);


Log.Information("Configuring web application {ApplicationContext} ...", Program.AppName);
try
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog();

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    builder.Services.RegisterApplication();
    builder.Services.RegisterPersistence(builder.Configuration);
    builder.Services.RegisterInfrastructer(builder.Configuration);





    var app = builder.Build();



    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.SetDatabaseMigrations();
        await app.UseSeedDataAsync();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();


    Log.Information("Starting web application {ApplicationContext}...", Program.AppName);
    app.Run();


}
catch (Exception ex)
{
    Log.Fatal(ex, "Program terminated unexpectedly {ApplicationContext}!", Program.AppName);
}
finally
{
    Log.CloseAndFlush();
}




Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
{

    return new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .Enrich.WithProperty("ApplicationContext", Program.AppName)
        .WriteTo.Console()
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

IConfiguration GetConfiguration()
{
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables();

    return builder.Build();
}

public partial class Program
{
    public const string AppName = "ContactService.Api";
}