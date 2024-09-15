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
        hostBuilder.AddLogging();
        service.AddPersistence(configuration);
        return service;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseLogging();
        app.UsePersistence();
        return app;
    }
}