using Guider.Application.Common.Mediatr;
using Microsoft.Extensions.DependencyInjection;

namespace Guider.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddMediatr();
        return service;
    }
}