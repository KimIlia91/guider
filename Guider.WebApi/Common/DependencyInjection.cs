using Guider.Common.Cors;
using Guider.Common.Errors;
using Guider.Common.Swagger;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Guider.Common;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddEndpointsApiExplorer();
        service.AddControllers();
        service.AddSingleton<ProblemDetailsFactory, GuiderApiProblemDetailsFactory>();
        service.AddCorsPolicy(configuration);
        service.AddHttpContextAccessor();
        service.AddRouting(opt => opt.LowercaseUrls = true);
        service.AddSwagger(configuration);
        return service;
    }

    public static WebApplication UsePresentation(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("CorsPolicy");
        app.UseExceptionHandler("/error");

        app.MapGet("/", context =>
        {
            context.Response.Redirect("/swagger");
            return Task.CompletedTask;
        });
        
        app.MapControllers();
        return app;
    }
}