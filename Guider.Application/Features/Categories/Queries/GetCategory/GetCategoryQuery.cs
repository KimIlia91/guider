using Guider.Application.Common.Models;
using Guider.Application.Features.Categories.Commands.Create;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.Specifications;
using Guider.Domain.Categories.ValueObjects;
using MediatR;

namespace Guider.Application.Features.Categories.Queries.GetCategory;

public sealed record GetCategoryQuery(Guid Id) : IRequest<CategoryResult>;

internal sealed class GetCategoryQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, CategoryResult>
{
    public async Task<CategoryResult> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository
            .GetAsync(new GetCategoryByIdNoTrackingSpec(CategoryId.Convert(request.Id)), cancellationToken);

        if (category is null)
            throw new ArgumentNullException(nameof(request.Id));

        return new CategoryResult(category.Id.Value, category.Name, category.Description);
    }
}