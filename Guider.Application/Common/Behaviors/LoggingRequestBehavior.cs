using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Guider.Application.Common.Behaviors;

public class LoggingRequestBehavior<TRequest, TResponse>(
    ILogger<LoggingRequestBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        logger.LogInformation(
            "Starting request {@RequestName} with parameters: {@Request}, {@DateTimeUtc}", 
            typeof(TRequest).Name, 
            request,
            DateTime.UtcNow);


        var result = await next();
        
        if (result.IsError)
            logger.LogError(
                "Request failure {@RequestName}, @{Error}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Errors,
                DateTime.UtcNow);
        
        logger.LogInformation(
            "Completed request {@RequestName} with result: {@Result}, {@DateTimeUtc}",
            typeof(TRequest).Name, 
            result,
            DateTime.UtcNow);

        return result;
    }
}