using Guider.Infrastructure.Logging;
using Guider.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Guider.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection service, 
        IConfiguration configuration,
        ConfigureHostBuilder hostBuilder)
    {
        service.AddPersistence(configuration);
        hostBuilder.AddLogging();
        return service;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseLogging();
        return app;
    }
}