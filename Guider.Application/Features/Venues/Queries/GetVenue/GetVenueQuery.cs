using Guider.Application.Common.Models;
using Guider.Application.Features.Tags.Commands.CreateTag;
using Guider.Application.Features.Venues.Commands.CreateVenue;
using Guider.Application.Features.Venues.Models;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;
using Guider.Domain.Venues.Specifications;
using MediatR;

namespace Guider.Application.Features.Venues.Queries.GetVenue;

public sealed record GetVenueQuery(Guid Id) : IRequest<VenueResult>;

internal sealed class GetVenueQueryHandler(
    IVenueRepository venueRepository) : IRequestHandler<GetVenueQuery, VenueResult>
{
    public async Task<VenueResult> Handle(GetVenueQuery request, CancellationToken cancellationToken)
    {
        var venue = await venueRepository
            .GetAsync(new GetVenueByIdWithTagsNoTrackingSpecification(VenueId.Convert(request.Id)), cancellationToken);

        if (venue is null)
            throw new ArgumentException(nameof(request.Id));

        return new VenueResult(
            venue.Id.Value,
            venue.Name,
            venue.Description,
            venue.Address,
            venue.Tags.ToList().ConvertAll(tag => new TagResult(tag.Id.Value, tag.Name, tag.Description)));
    }
}