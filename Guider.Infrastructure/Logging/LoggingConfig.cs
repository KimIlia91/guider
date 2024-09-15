using Microsoft.AspNetCore.Builder;
using Serilog;

namespace Guider.Infrastructure.Logging;

internal static class LoggingConfig
{
    public static ConfigureHostBuilder AddLogging(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        return hostBuilder;
    }

    public static WebApplication UseLogging(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        return app;
    }
}