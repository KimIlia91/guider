using Guider.Application.Features.Categories.Commands.Create;
using Guider.Application.Features.Categories.Commands.Delete;
using Guider.Application.Features.Categories.Commands.Update;
using Guider.Application.Features.Categories.Models;
using Guider.Application.Features.Categories.Queries.GetAll;
using Guider.Application.Features.Categories.Queries.GetById;
using Guider.Common.Models.Categories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Guider.Controllers;

/// <summary>
/// Category controller
/// </summary>
public class CategoryController(ISender mediatr) : ApiController(mediatr)
{
    /// <summary>
    /// Create new category
    /// </summary>
    /// <param name="request">CreateCategoryRequest DTO model</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>CategoryResult DTO model</returns>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCategory(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateCategoryCommand(request.Name, request.Description);
        var result = await Mediatr.Send(command, cancellationToken);
        return result.Match(Ok, Problem);
    }

    /// <summary>
    /// Update category
    /// </summary>
    /// <param name="request">UpdateCategoryRequest DTO model</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>CategoryResult DTO model</returns>
    [HttpPut]
    [ProducesResponseType(typeof(CategoryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCategoryCommand(request.Id, request.Name, request.Description);
        var result = await Mediatr.Send(command, cancellationToken);
        return result.Match(Ok, Problem);
    }

    /// <summary>
    /// Get all categories
    /// </summary>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>List of CategoryResult DTO models</returns>
    [HttpGet]
    [ProducesResponseType(typeof(CategoryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var query = new GetCategoriesQuery();
        var result = await Mediatr.Send(query, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Get category by id
    /// </summary>
    /// <param name="id">ID of category to return</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    /// <returns>CategoryResult DTO model</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CategoryResult), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategory(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetCategoryQuery(id);
        var result = await Mediatr.Send(query, cancellationToken);
        return result.Match(Ok, Problem);
    }

    /// <summary>
    /// Delete category by id
    /// </summary>
    /// <param name="id">ID of category to delete</param>
    /// <param name="cancellationToken">Token to cancel operation</param>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteCategoryCommand(id);
        var result = await Mediatr.Send(command, cancellationToken);
        return result.Match(_ => NoContent(), Problem);
    }
}