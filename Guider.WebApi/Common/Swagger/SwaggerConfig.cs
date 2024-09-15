using System.Reflection;
using Guider.Common.Options;
using Microsoft.OpenApi.Models;

namespace Guider.Common.Swagger;

/// <summary>
/// Provides extension methods for configuring Swagger in an ASP.NET Core application.
/// </summary>
internal static class SwaggerConfig
{
    /// <summary>
    /// Configures Swagger services for the application.
    /// </summary>
    /// <param name="service">The IServiceCollection to add the Swagger services to.</param>
    /// <param name="configuration">The IConfiguration instance to use for reading configuration settings.</param>
    public static void AddSwagger(this IServiceCollection service, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        var apiOptions = configuration.GetSection("API").Get<ApiOptions>() ?? new ApiOptions();
        
        service.AddSwaggerGen(config =>
        {
            config.SwaggerDoc(apiOptions.Version, new OpenApiInfo { Title = apiOptions.Name, Version = apiOptions.Version});
            config.DescribeAllParametersInCamelCase();

            var xmlFile = $"{assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            config.IncludeXmlComments(xmlPath, true);
            
            var referencedAssembliesNames = assembly.GetReferencedAssemblies().Distinct();
            
            foreach (var assemblyName in referencedAssembliesNames)
            {
                var relatedXmlFile = $"{assemblyName.Name}.xml";
                var relatedXmlPath = Path.Combine(AppContext.BaseDirectory, relatedXmlFile);
                
                if (File.Exists(relatedXmlPath))
                    config.IncludeXmlComments(relatedXmlPath, true);
            }
        });
    }
}