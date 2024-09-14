using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Guider.Controllers;

/// <summary>
/// Controller for handling error responses.
/// </summary>
/// <param name="mediatr">MediatR service for handling requests.</param>
/// <param name="contextAccessor">Accesses the HTTP context.</param>
[Consumes("application/problem+json")]
public class ErrorController(
    ISender mediatr, 
    IHttpContextAccessor contextAccessor) : ApiController(mediatr)
{
    /// <summary>
    /// Returns a Problem Details response with error information.
    /// </summary>
    /// <returns>A Problem Details response.</returns>
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = contextAccessor.HttpContext?.Features.GetRequiredFeature<IExceptionHandlerFeature>();
        return Problem(detail: exception?.Error.Message, instance: exception?.Path);
    }
}