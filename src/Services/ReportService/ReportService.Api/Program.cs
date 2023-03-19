using ReportService.Application;
using ReportService.Persistence;
using Serilog;

IConfiguration configuration = GetConfiguration();
Log.Logger = CreateSerilogLogger(configuration);


Log.Information("Configuring web application {ApplicationContext} ...", Program.AppName);

try
{
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
    public const string AppName = "ReportService.Api";
}