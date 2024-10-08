﻿using Guider.Application.Common.Models;
using Guider.Application.Features.Tags.Commands.Create;
using Guider.Application.Features.Tags.Commands.Delete;
using Guider.Application.Features.Tags.Commands.Update;
using Guider.Application.Features.Tags.Queries.GetAll;
using Guider.Application.Features.Tags.Queries.GetById;
using Guider.Common.Models.Tags;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Guider.Controllers;

/// <summary>
/// Controller to manage tag-related operations
/// </summary>
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
      return result.Match(Ok, Problem);
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
      return result.Match(Ok, Problem);
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
      return result.Match(Ok, Problem);
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
      return result.Match(Ok, Problem);
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
      var result = await Mediatr.Send(command, cancellationToken);
      return result.Match(_ => NoContent(), Problem);
   }
}