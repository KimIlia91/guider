using ErrorOr;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.Specifications;
using MediatR;

namespace Guider.Application.Features.Categories.Queries.GetAll;

public sealed record GetCategoriesQuery : IRequest<ErrorOr<List<CategoryResult>>>;

internal sealed class GetCategoriesQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, ErrorOr<List<CategoryResult>>>
{
    public async Task<ErrorOr<List<CategoryResult>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository
            .GetAllAsync(new GetCategoriesNoTrackingSpecification(), cancellationToken: cancellationToken);
        
        return categories.ConvertAll(category =>
            new CategoryResult(category.Id.Value, category.Name, category.Description));
    }
}