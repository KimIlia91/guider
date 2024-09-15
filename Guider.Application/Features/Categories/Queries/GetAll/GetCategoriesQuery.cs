using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.Specifications;
using MediatR;

namespace Guider.Application.Features.Categories.Queries.GetAll;

public sealed record GetCategoriesQuery : IRequest<List<CategoryResult>>;

internal sealed class GetCategoriesQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, List<CategoryResult>>
{
    public async Task<List<CategoryResult>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository
            .GetAllAsync(new GetCategoriesNoTrackingSpecification(), cancellationToken: cancellationToken);
        
        return categories.ConvertAll(category =>
            new CategoryResult(category.Id.Value, category.Name, category.Description));
    }
}