using Guider.Application.Common.Models;
using Guider.Application.Features.Categories.Commands.Create;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using MediatR;

namespace Guider.Application.Features.Categories.Queries.GetCategories;

public sealed record GetCategoriesQuery : IRequest<List<CategoryResult>>;

internal sealed class GetCategoriesQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, List<CategoryResult>>
{
    public async Task<List<CategoryResult>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(cancellationToken: cancellationToken);
        return categories.ConvertAll(category =>
            new CategoryResult(category.Id.Value, category.Name, category.Description));
    }
}