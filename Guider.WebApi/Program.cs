using Guider.Application;
using Guider.Common;
using Guider.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration, builder.Host);

var app = builder.Build()
    .UsePresentation()
    .UseInfrastructure();

app.Run();
