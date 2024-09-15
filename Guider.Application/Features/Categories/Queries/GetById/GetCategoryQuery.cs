using ErrorOr;
using Guider.Application.Features.Categories.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.Specifications;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Errors;
using MediatR;

namespace Guider.Application.Features.Categories.Queries.GetById;

public sealed record GetCategoryQuery(Guid Id) : IRequest<ErrorOr<CategoryResult>>;

internal sealed class GetCategoryQueryHandler(
    ICategoryRepository categoryRepository) : IRequestHandler<GetCategoryQuery, ErrorOr<CategoryResult>>
{
    public async Task<ErrorOr<CategoryResult>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        var category = await categoryRepository
            .GetAsync(new GetCategoryByIdNoTrackingSpec(CategoryId.Convert(request.Id)), cancellationToken);

        if (category is null)
            return Errors.Category.NotFoundById(request.Id);

        return new CategoryResult(category.Id.Value, category.Name, category.Description);
    }
}