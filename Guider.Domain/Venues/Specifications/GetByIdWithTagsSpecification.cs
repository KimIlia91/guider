using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;

namespace Guider.Domain.Venues.Specifications;

public sealed class GetByIdWithTagsSpecification : Specification<Venue, VenueId>
{
    public GetByIdWithTagsSpecification(VenueId id) 
        : base(venue => venue.Id == id)
    {
        AddInclude(venue => venue.Tags);
    }
}