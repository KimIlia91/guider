using Guider.Domain.Common.Specifications;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Domain.Tags.Specifications;

public sealed class GetTagsNoTrackingSpecification
    : Specification<Tag, TagId>
{
    public GetTagsNoTrackingSpecification() : base(null)
    {
        IsAsNoTracking = true;
    }
}