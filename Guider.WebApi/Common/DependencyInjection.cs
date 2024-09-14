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

    public static void UsePresentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("CorsPolicy");
        app.UseHttpsRedirection();
        app.UseExceptionHandler("/error");
        app.MapControllers();
        app.Run();
    }
}