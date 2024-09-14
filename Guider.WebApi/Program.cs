using Guider.Application;
using Guider.Common;
using Guider.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPresentation(builder.Configuration)
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder
    .Build()
    .UsePresentation();


