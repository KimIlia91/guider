using Guider.Domain.Common.Specifications;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Domain.Tags.Specifications;

public sealed class GetTagByIdNoTrackingSpecification
    : Specification<Tag, TagId>
{
    public GetTagByIdNoTrackingSpecification(TagId id) : base(t => t.Id == id)
    {
        IsAsNoTracking = true;
    }
}