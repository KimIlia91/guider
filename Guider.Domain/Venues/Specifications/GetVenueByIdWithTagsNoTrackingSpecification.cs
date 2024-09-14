using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;

namespace Guider.Domain.Venues.Specifications;

public sealed class GetVenueByIdWithTagsNoTrackingSpecification
    : Specification<Venue, VenueId>
{
    public GetVenueByIdWithTagsNoTrackingSpecification(VenueId id) : base(venue => venue.Id == id)
    {
        AddInclude(v => v.Tags);

        IsAsNoTracking = true;
    }
}