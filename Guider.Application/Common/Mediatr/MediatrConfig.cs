using System.Reflection;
using Guider.Application.Common.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Guider.Application.Common.Mediatr;

internal static class MediatrConfig
{
    public static void AddMediatr(this IServiceCollection service)
    {
        var assembly = Assembly.GetExecutingAssembly();
        service.AddMediatR(cnf =>
        {
            cnf.RegisterServicesFromAssembly(assembly);
            cnf.AddBehavior(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        });
    }
}