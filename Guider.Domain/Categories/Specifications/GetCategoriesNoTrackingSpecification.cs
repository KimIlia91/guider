using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Specifications;

namespace Guider.Domain.Categories.Specifications;

public sealed class GetCategoriesNoTrackingSpecification
    : Specification<Category, CategoryId>
{
    public GetCategoriesNoTrackingSpecification() : base(null)
    {
        IsAsNoTracking = true;
    }
}