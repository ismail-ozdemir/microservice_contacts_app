using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using ReportService.Application;
using ReportService.Infrastructure;
using ReportService.Persistence;
using Serilog;

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
    IHealthChecksBuilder hcBuilder = builder.Services.AddHealthChecks();


    builder.Services.RegisterApplicationServices();
    builder.Services.RegisterPersistenceServices(configuration, hcBuilder);
    builder.Services.RegisterInfrastructureServices(configuration, hcBuilder);

    var app = builder.Build();
    app.UseHealthChecks("/health", new HealthCheckOptions { ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse });
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        await app.SetDatabaseMigrations();
    }

    app.UseEventBus();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    Log.Information("Starting web application {ApplicationContext}...", Program.AppName);
    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "Program terminated unexpectedly {ApplicationContext}!", Program.AppName);
    Console.ReadLine();

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
    public const string AppName = "ReportService.Api";
}


