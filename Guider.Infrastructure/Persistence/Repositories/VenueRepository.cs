using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence.Repositories;

internal class VenueRepository(ApplicationDbContext dbContext) 
    : Repository<Venue, VenueId>(dbContext), IVenueRepository
{
    public Task<Venue?> GetAsync(Specification<Venue, VenueId> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await DbSet
            .AnyAsync(v => v.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}