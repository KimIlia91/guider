﻿using Guider.Domain.Common.Specifications;
using Guider.Domain.Entities.Venues.ValueObjects;

namespace Guider.Domain.Venues;

public interface IVenueRepository
{
    Task<Venue?> GetAsync(Specification<Venue, VenueId> specification, CancellationToken cancellationToken);

    Task<List<Venue>> GetAllAsync(
        Specification<Venue, VenueId>? specification = null, 
        CancellationToken cancellationToken = default);

    Task UpdateAsync(Venue venue, CancellationToken cancellationToken);

    Task CreateAsync(Venue venue, CancellationToken cancellationToken);

    Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken);
}