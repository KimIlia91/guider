using Guider.Application.Common.Models;
using Guider.Application.Features.Tags.Commands.CreateTag;
using Guider.Application.Features.Tags.Commands.DeleteTag;
using Guider.Application.Features.Tags.Commands.UpdateTag;
using Guider.Application.Features.Tags.Queries.GetTag;
using Guider.Application.Features.Tags.Queries.GetTags;
using Guider.Common.Models.Tags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Guider.Controllers;

/// <summary>
/// Tag controller
/// </summary>
[Produces("application/json")]
[Consumes("application/json")]
public class TagController(ISender mediatr) : ApiController(mediatr)
{
   /// <summary>
   /// Create new tag
   /// </summary>
   /// <param name="request">CreateTagRequest DTO model</param>
   /// <param name="cancellationToken">Token to cancel operation</param>
   /// <returns>TagResult DTO model</returns>
   [HttpPost]
   [ProducesResponseType(typeof(TagResult), StatusCodes.Status200OK)]
   public async Task<IActionResult> CreateTag(CreateTagRequest request, CancellationToken cancellationToken)
   {
      var command = new CreateTagCommand(request.Name, request.Description);
      var result = await Mediatr.Send(command, cancellationToken);
      return Ok(result);
   }

   /// <summary>
   /// Update tag
   /// </summary>
   /// <param name="request">UpdateTagRequest DTO model</param>
   /// <param name="cancellationToken">Token to cancel operation</param>
   /// <returns>TagResult DTO model</returns>
   [HttpPut]
   [ProducesResponseType(typeof(TagResult), StatusCodes.Status200OK)]
   public async Task<IActionResult> UpdateTag(UpdateTagRequest request, CancellationToken cancellationToken)
   {
      var command = new UpdateTagCommand(request.Id, request.Name, request.Description);
      var result = await Mediatr.Send(command, cancellationToken);
      return Ok(result);
   }

   /// <summary>
   /// Get all tags
   /// </summary>
   /// <param name="cancellationToken">Token to cancel operation</param>
   /// <returns>TagResult list DTO models</returns>
   [HttpGet]
   [ProducesResponseType(typeof(List<TagResult>), StatusCodes.Status200OK)]
   public async Task<IActionResult> GetTags(CancellationToken cancellationToken)
   {
      var query = new GetTagsQuery();
      var result = await Mediatr.Send(query, cancellationToken);
      return Ok(result);
   }

   /// <summary>
   /// Get tag by id
   /// </summary>
   /// <param name="id">ID of tag to return</param>
   /// <param name="cancellationToken">Token to cancel operation</param>
   /// <returns>TagResult DTO model</returns>
   [HttpGet("{id:guid}")]
   [ProducesResponseType(typeof(TagResult), StatusCodes.Status200OK)]
   public async Task<IActionResult> GetTag(Guid id, CancellationToken cancellationToken)
   {
      var query = new GetTagQuery(id);
      var result = await Mediatr.Send(query, cancellationToken);
      return Ok(result);
   }

   /// <summary>
   /// Delete tag by id
   /// </summary>
   /// <param name="id">ID of tag to delete</param>
   /// <param name="cancellationToken">Token to cancel operation</param>
   /// <returns>Status code 204 NO CONTENT</returns>
   [HttpDelete("{id:guid}")]
   [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
   public async Task<IActionResult> DeleteTag(Guid id, CancellationToken cancellationToken)
   {
      var command = new DeleteTagCommand(id);
      await Mediatr.Send(command, cancellationToken);
      return NoContent();
   }
}