using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;

namespace Guider.Domain.Venues.Specifications;

public sealed class GetVenuesWithTagsSpecification : Specification<Venue, VenueId>
{
    public GetVenuesWithTagsSpecification() : base(null)
    {
        AddInclude(venue => venue.Tags);
    }
}