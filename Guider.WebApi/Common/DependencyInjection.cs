using Guider.Common.Cors;
using Guider.Common.Swagger;

namespace Guider.Common;

internal static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection service, IConfiguration configuration)
    {
        service.AddEndpointsApiExplorer();
        service.AddControllers();
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

        app.UseHttpsRedirection();
        app.UseExceptionHandler("/error");
        app.MapControllers();
        app.Run();
    }
}