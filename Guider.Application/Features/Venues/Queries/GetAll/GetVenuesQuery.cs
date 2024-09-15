using Guider.Application.Common.Models;
using Guider.Application.Features.Venues.Models;
using Guider.Domain.Venues;
using Guider.Domain.Venues.Specifications;
using MediatR;

namespace Guider.Application.Features.Venues.Queries.GetAll;

public sealed record GetVenuesQuery : IRequest<List<VenueResult>>;

internal sealed class GetVenueQueryHandler(
    IVenueRepository venueRepository) : IRequestHandler<GetVenuesQuery, List<VenueResult>>
{
    public async Task<List<VenueResult>> Handle(GetVenuesQuery request, CancellationToken cancellationToken)
    {
        var venues = await venueRepository
            .GetAllAsync(new GetVenuesWithTagsSpecification(), cancellationToken);

        return venues.ConvertAll(venue =>
            new VenueResult(
                venue.Id.Value,
                venue.Name,
                venue.Description,
                venue.Address,
                venue.Tags.ToList().ConvertAll(tag => new TagResult(tag.Id.Value, tag.Name, tag.Description))
            ));
    }
}