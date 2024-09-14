using System.Diagnostics;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace Guider.Common.Errors;

/// <summary>
/// Factory class for creating ProblemDetails and ValidationProblemDetails according to the Guider API specifications.
/// </summary>
public class GuiderApiProblemDetailsFactory(
    IOptions<ApiBehaviorOptions> options,
    IOptions<ProblemDetailsOptions>? problemDetailsOptions = null) 
    : ProblemDetailsFactory
{
    private readonly ApiBehaviorOptions _options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    private readonly Action<ProblemDetailsContext>? _configure = problemDetailsOptions?.Value?.CustomizeProblemDetails;
    
    /// <summary>
    /// Creates an instance of <see cref="ProblemDetails"/> according to the Guider API specifications.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="statusCode">The HTTP status code to be used for the problem details. Defaults to null.</param>
    /// <param name="title">A short, human-readable summary of the problem type. Defaults to null.</param>
    /// <param name="type">A URI reference that identifies the problem type. Defaults to null.</param>
    /// <param name="detail">A human-readable explanation specific to this occurrence of the problem. Defaults to null.</param>
    /// <param name="instance">A URI reference that identifies the specific occurrence of the problem. Defaults to null.</param>
    /// <returns>A <see cref="ProblemDetails"/> instance containing the problem details.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public override ProblemDetails CreateProblemDetails(
        HttpContext httpContext, 
        int? statusCode = null,
        string? title = null,
        string? type = null, 
        string? detail = null, 
        string? instance = null)
    {
        statusCode ??= 500;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Creates an instance of <see cref="ValidationProblemDetails"/> according to the Guider API specifications.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="modelStateDictionary">The <see cref="ModelStateDictionary"/> containing the model validation state.</param>
    /// <param name="statusCode">The HTTP status code to be used for the validation problem details. Defaults to null.</param>
    /// <param name="title">A short, human-readable summary of the validation problem type. Defaults to null.</param>
    /// <param name="type">A URI reference that identifies the validation problem type. Defaults to null.</param>
    /// <param name="detail">A human-readable explanation specific to this occurrence of the validation problem. Defaults to null.</param>
    /// <param name="instance">A URI reference that identifies the specific occurrence of the validation problem. Defaults to null.</param>
    /// <returns>A <see cref="ValidationProblemDetails"/> instance containing the validation problem details.</returns>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary, 
        int? statusCode = null, 
        string? title = null, 
        string? type = null,
        string? detail = null, 
        string? instance = null)
    {
        ArgumentNullException.ThrowIfNull(modelStateDictionary);

        statusCode ??= 400;

        var problemDetails = new ValidationProblemDetails(modelStateDictionary)
        {
            Status = statusCode,
            Type = type,
            Detail = detail,
            Instance = instance,
        };

        if (title != null)
            problemDetails.Title = title;

        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

        return problemDetails;
    }

    /// <summary>
    /// Applies default values to the <see cref="ProblemDetails"/> instance based on the Guider API specifications.
    /// </summary>
    /// <param name="httpContext">The <see cref="HttpContext"/> for the current request.</param>
    /// <param name="problemDetails">The <see cref="ProblemDetails"/> instance to which the defaults will be applied.</param>
    /// <param name="statusCode">The HTTP status code to be used for the problem details.</param>
    /// <exception cref="NotImplementedException">Always thrown.</exception>
    private void ApplyProblemDetailsDefaults(
        HttpContext httpContext, 
        ProblemDetails problemDetails, 
        int statusCode)
    {
        problemDetails.Status ??= statusCode;

        if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link;
        }

        var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
        if (traceId != null)
            problemDetails.Extensions["traceId"] = traceId;

        if (httpContext?.Items[HttpContextItemKeys.Errors] is not List<Error> errors) return;
        
        var errorDictionary = errors
            .GroupBy(e => e.Code)
            .ToDictionary(
                group => group.Key, 
                group => group.Select(e => e.Description).ToList()
            );

        problemDetails.Extensions.Add("errors", errorDictionary);
    }
}