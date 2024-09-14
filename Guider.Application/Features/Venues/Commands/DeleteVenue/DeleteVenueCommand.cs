using Guider.Application.Common.Repositories;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Venues;
using Guider.Domain.Venues.Specifications;
using MediatR;

namespace Guider.Application.Features.Venues.Commands.DeleteVenue;

public sealed record DeleteVenueCommand(Guid Id) : IRequest;

internal sealed class DeleteVenueCommandHandler(
    IVenueRepository venueRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteVenueCommand>
{
    public async Task Handle(DeleteVenueCommand request, CancellationToken cancellationToken)
    {
        var venue = await venueRepository
            .GetAsync(new GetVenueByIdSpecification(VenueId.Convert(request.Id)), cancellationToken);

        if (venue is null)
            throw new ArgumentException(nameof(request.Id));
        
        venue.Delete();
        await venueRepository.UpdateAsync(venue, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
    }
}