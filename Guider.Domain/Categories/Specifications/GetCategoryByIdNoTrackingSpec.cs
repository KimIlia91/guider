using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Specifications;

namespace Guider.Domain.Categories.Specifications;

public sealed class GetCategoryByIdNoTrackingSpec
    : Specification<Category, CategoryId>
{
    public GetCategoryByIdNoTrackingSpec(CategoryId id) : base(c => c.Id == id)
    {
        IsAsNoTracking = true;
    }
}