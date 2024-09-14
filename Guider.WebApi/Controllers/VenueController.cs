using Guider.Application.Features.Categories.Commands.DeleteCategory;
using Guider.Application.Features.Venues.Commands.CreateVenue;
using Guider.Application.Features.Venues.Commands.UpdateVenue;
using Guider.Application.Features.Venues.Models;
using Guider.Application.Features.Venues.Queries.GetVenue;
using Guider.Application.Features.Venues.Queries.GetVenues;
using Guider.Common.Models.Venues;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Guider.Controllers;

/// <summary>
/// Venue controller
/// </summary>
public class VenueController(ISender mediatr) : ApiController(mediatr)
{
    /// <summary>
    /// Create new venue
    /// </summary>
    /// <param name="request">CreateVenueRequest DTO model</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>VenueResult DTO model</returns>
    [HttpPost]
    [ProducesResponseType(typeof(VenueResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateVenue(CreateVenueRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateVenueCommand(
            request.Name, request.Description, request.CategoryId, request.Address, request.TagIds ?? []);
        var result = await Mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Update venue
    /// </summary>
    /// <param name="request">UpdateVenueRequest DTO model</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>VenueResult DTO model</returns>
    [HttpPut]
    [ProducesResponseType(typeof(VenueResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateVenue(UpdateVenueRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateVenueCommand(
            request.Id,
            request.Name,
            request.Description,
            request.Address,
            request.CategoryId,
            request.TagIds ?? []);

        var result = await Mediatr.Send(command, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get all venues
    /// </summary>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>VenueResult list DTO models</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<VenueResult>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVenues(CancellationToken cancellationToken)
    {
        var query = new GetVenuesQuery();
        var result = await Mediatr.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get venue by id
    /// </summary>
    /// <param name="id">ID of venue to return</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>VenueResult DTO model</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VenueResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetVenue(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetVenueQuery(id);
        var result = await Mediatr.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Delete venue by id
    /// </summary>
    /// <param name="id">ID of venue to delete</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>Status code 204 NO CONTENT</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteVenue(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);
        await Mediatr.Send(command, cancellationToken);
        return NoContent();
    }
}