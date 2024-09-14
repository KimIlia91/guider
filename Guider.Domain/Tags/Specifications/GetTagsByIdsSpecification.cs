using Guider.Domain.Common.Specifications;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Domain.Tags.Specifications;

public sealed class GetTagsByIdsSpecification(List<TagId> ids) 
    : Specification<Tag, TagId>(tag => ids.Contains(tag.Id));