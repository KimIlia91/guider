using MediatR;
using Microsoft.Extensions.Logging;

namespace Guider.Application.Common.Behaviors;

internal sealed class UnhandledExceptionBehaviour<TRequest, TResponse>(
    ILogger<UnhandledExceptionBehaviour<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex, 
                "CleanArchitecture Request: Unhandled Exception for Request {Name} {@Request}",
                typeof(TRequest).Name,
                request);

            throw;
        }
    }
}