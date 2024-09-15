using Guider.Common.Options;

namespace Guider.Common.Cors;

/// <summary>
/// Provides extension methods for configuring CORS (Cross-Origin Resource Sharing) policies.
/// </summary>
public static class CorsConfig
{
    /// <summary>
    /// Adds and configures a CORS policy to the IServiceCollection.
    /// </summary>
    /// <param name="service">The IServiceCollection to add the CORS policy to.</param>
    /// <param name="configuration">The IConfiguration instance containing CORS settings.</param>
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