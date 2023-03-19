
using ContactService.Grpc.Services;
using ContactService.Application;
using ContactService.Persistence.Extentions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGrpc();

builder.Services.RegisterApplication();
builder.Services.RegisterPersistence(builder.Configuration);


var app = builder.Build();
app.MapGrpcService<GrpcContactService>();


app.Run();
