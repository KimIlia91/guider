using Guider.Application.Common;
using Guider.Application.Common.Models;
using Guider.Application.Features.Venues.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using Guider.Domain.Tags.ValueObjects;
using Guider.Domain.Venues;
using MediatR;

namespace Guider.Application.Features.Venues.Commands.CreateVenue;

public sealed record CreateVenueCommand(
    string Name,
    string Description,
    Guid CategoryId,
    string Address,
    List<Guid> TagIds) : IRequest<VenueResult>;

internal sealed class CreateVenueCommandHandler(
    IVenueRepository venueRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateVenueCommand, VenueResult>
{
    public async Task<VenueResult> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        if (await venueRepository.ExistByNameAsync(request.Name, cancellationToken))
            throw new ArgumentException(nameof(request.Name));

        var category = await categoryRepository
            .GetByIdAsync(CategoryId.Convert(request.CategoryId), cancellationToken);

        if (category is null)
            throw new ArgumentException(nameof(request.CategoryId));
        
        var venue = Venue.Create(request.Name, category.Id, request.Address, request.Description);

        if (request.TagIds.Count != 0)
        {
            var tags = await tagRepository
                .GetAllAsync(new GetTagsByIdsSpecification(request.TagIds.ConvertAll(TagId.Convert)), cancellationToken);

            if (tags.Count < request.TagIds.Count)
                throw new ArgumentException(nameof(request.TagIds));

            venue.AddTags(tags);
        }

        await venueRepository.CreateAsync(venue, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        
        return new VenueResult(
            venue.Id.Value, 
            venue.Name, 
            venue.Description, 
            venue.Address, 
            venue.Tags.ToList().ConvertAll(tag => new TagResult(tag.Id.Value, tag.Name, tag.Description)));
    }
}