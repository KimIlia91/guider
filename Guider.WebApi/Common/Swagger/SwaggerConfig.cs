using System.Reflection;
using Guider.Common.Options;
using Microsoft.OpenApi.Models;

namespace Guider.Common.Swagger;

internal static class SwaggerConfig
{
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