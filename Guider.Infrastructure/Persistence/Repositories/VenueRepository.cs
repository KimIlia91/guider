using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;

namespace Guider.Infrastructure.Persistence.Repositories;

public class VenueRepository : IVenueRepository
{
    public Task<Venue?> GetAsync(Specification<Venue, VenueId> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Venue>> GetAllAsync(Specification<Venue, VenueId>? specification = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Venue venue, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Venue venue, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}