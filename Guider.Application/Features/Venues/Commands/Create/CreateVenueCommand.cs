using ErrorOr;
using Guider.Application.Common;
using Guider.Application.Common.Models;
using Guider.Application.Features.Venues.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Errors;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using Guider.Domain.Tags.ValueObjects;
using Guider.Domain.Venues;
using MediatR;

namespace Guider.Application.Features.Venues.Commands.Create;

public sealed record CreateVenueCommand(
    string Name,
    string Description,
    Guid CategoryId,
    string Address,
    List<Guid> TagIds) : IRequest<ErrorOr<VenueResult>>;

internal sealed class CreateVenueCommandHandler(
    IVenueRepository venueRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateVenueCommand, ErrorOr<VenueResult>>
{
    public async Task<ErrorOr<VenueResult>> Handle(CreateVenueCommand request, CancellationToken cancellationToken)
    {
        if (await venueRepository.ExistByNameAsync(request.Name, cancellationToken))
            return Errors.Venue.NameConflict(request.Name);

        var category = await categoryRepository
            .GetByIdAsync(CategoryId.Convert(request.CategoryId), cancellationToken);

        if (category is null) return Errors.Category.NotFoundById(request.CategoryId);
        
        var venue = Venue.Create(request.Name, category.Id, request.Address, request.Description);

        if (request.TagIds.Count != 0)
        {
            var tagIdsToFetch = request.TagIds.ConvertAll(TagId.Convert);
            var tags = await tagRepository
                .GetAllAsync(new GetTagsByIdsSpecification(tagIdsToFetch), cancellationToken);

            if (tags.Count < request.TagIds.Count)
            {
                var foundTagIds = tags.Select(tag => tag.Id).ToHashSet();
                var notFoundTagIds = tagIdsToFetch
                    .Where(id => !foundTagIds.Contains(id))
                    .Select(t => t.Value)
                    .ToList();

                return Errors.Tag.NotFoundSomeIds(notFoundTagIds);
            }

            venue.AddTags(tags);
        }

        await venueRepository.CreateAsync(venue, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        
        return new VenueResult(
            venue.Id.Value, 
            venue.Name, 
            venue.CategoryId.Value,
            venue.Description, 
            venue.Address, 
            venue.Tags.ToList().ConvertAll(tag => new TagResult(tag.Id.Value, tag.Name, tag.Description)));
    }
}