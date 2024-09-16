using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;
using Guider.Infrastructure.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence.Repositories;

internal class VenueRepository(ApplicationDbContext dbContext) 
    : Repository<Venue, VenueId>(dbContext), IVenueRepository
{
}