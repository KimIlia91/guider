using ErrorOr;
using Guider.Application.Common.Models;
using Guider.Application.Features.Venues.Models;
using Guider.Domain.Common.Errors;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;
using Guider.Domain.Venues.Specifications;
using MediatR;

namespace Guider.Application.Features.Venues.Queries.GetById;

public sealed record GetVenueQuery(Guid Id) : IRequest<ErrorOr<VenueResult>>;

internal sealed class GetVenueQueryHandler(
    IVenueRepository venueRepository) : IRequestHandler<GetVenueQuery, ErrorOr<VenueResult>>
{
    public async Task<ErrorOr<VenueResult>> Handle(GetVenueQuery request, CancellationToken cancellationToken)
    {
        var venue = await venueRepository
            .GetAsync(new GetVenueByIdWithTagsNoTrackingSpecification(VenueId.Convert(request.Id)), cancellationToken);

        if (venue is null)
            return Errors.Venue.NotFoundById(request.Id);

        return new VenueResult(
            venue.Id.Value,
            venue.Name,
            venue.CategoryId.Value,
            venue.Description,
            venue.Address,
            venue.Tags.ToList().ConvertAll(tag => new TagResult(tag.Id.Value, tag.Name, tag.Description)));
    }
}