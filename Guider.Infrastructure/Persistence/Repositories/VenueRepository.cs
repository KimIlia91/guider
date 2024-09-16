using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;

namespace Guider.Infrastructure.Persistence.Repositories;

internal class VenueRepository(ApplicationDbContext dbContext) 
    : Repository<Venue, VenueId>(dbContext), IVenueRepository
{
}