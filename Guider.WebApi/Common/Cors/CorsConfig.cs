using Guider.Common.Options;

namespace Guider.Common.Cors;

/// <summary>
/// CORS configuration
/// </summary>
public static class CorsConfig
{
    /// <summary>
    /// Add cors config extension method
    /// </summary>
    public static void AddCorsPolicy(this IServiceCollection service, IConfiguration configuration)
    {
        var corsOptions = configuration.GetSection("CORS").Get<CorsOptions>() ?? new CorsOptions();

        service.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder =>
                {
                    if (corsOptions.AllowedHosts.Count != 0)
                    {
                        builder.WithOrigins([..corsOptions.AllowedHosts]);
                    }
                    else
                    {
                        builder.AllowAnyOrigin();
                    }

                    if (corsOptions.AllowedHeaders.Count != 0)
                    {
                        builder
                            .WithHeaders([..corsOptions.AllowedHeaders]);
                    }
                    else
                    {
                        builder.AllowAnyHeader();
                    }

                    builder
                        .WithExposedHeaders("Content-Disposition")
                        .AllowAnyMethod();
                    //.AllowCredentials();
                }
            );
        });
    }
}