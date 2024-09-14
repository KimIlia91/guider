using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;

namespace Guider.Domain.Venues.Specifications;

public sealed class GetVenueByIdSpecification(VenueId id) 
    : Specification<Venue, VenueId>(venue => venue.Id == id);