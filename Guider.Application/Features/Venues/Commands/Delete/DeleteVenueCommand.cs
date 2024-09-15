using ErrorOr;
using Guider.Application.Common;
using Guider.Domain.Common.Errors;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;
using MediatR;

namespace Guider.Application.Features.Venues.Commands.Delete;

public sealed record DeleteVenueCommand(Guid Id) : IRequest<ErrorOr<VenueId>>;

internal sealed class DeleteVenueCommandHandler(
    IVenueRepository venueRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteVenueCommand, ErrorOr<VenueId>>
{
    public async Task<ErrorOr<VenueId>> Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
    {
        var venue = await venueRepository
            .GetByIdAsync(VenueId.Convert(request.Id), cancellationToken);

        if (venue is null)
            return Errors.Venue.NotFoundById(request.Id);
        
        venue.Delete();
        venueRepository.Update(venue);
        await unitOfWork.SaveAsync(cancellationToken);
        return venue.Id;
    }
}