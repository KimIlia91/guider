using ErrorOr;
using Guider.Application.Common;
using Guider.Application.Common.Models;
using Guider.Application.Features.Venues.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Errors;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using Guider.Domain.Tags.ValueObjects;
using Guider.Domain.Venues;
using Guider.Domain.Venues.Specifications;
using MediatR;

namespace Guider.Application.Features.Venues.Commands.Update;

public sealed record UpdateVenueCommand(
    Guid Id, 
    string Name, 
    string Description, 
    string Address, 
    Guid CategoryId, 
    List<Guid> TagIds) : IRequest<ErrorOr<VenueResult>>;

internal sealed class UpdateVenueCommandHandler(
    IVenueRepository venueRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateVenueCommand, ErrorOr<VenueResult>>
{
    public async Task<ErrorOr<VenueResult>> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
    {
        var venue = await venueRepository
            .GetAsync(new GetByIdWithTagsSpecification(VenueId.Convert(request.Id)), cancellationToken);

        if (venue is null) return Errors.Venue.NotFoundById(request.Id);

        var categoryId = CategoryId.Convert(request.CategoryId);

        if (venue.CategoryId != categoryId)
        {
            if (!await categoryRepository.ExistByIdAsync(categoryId, cancellationToken))
                return Errors.Category.NotFoundById(request.CategoryId);
            
            venue.UpdateCategory(categoryId);
        }

        if (request.TagIds.Count != 0)
        {
            var tagIdsToFetch = request.TagIds.ConvertAll(TagId.Convert);
            var newTags = await tagRepository
                .GetAllAsync(new GetTagsByIdsSpecification(tagIdsToFetch), cancellationToken);

            if (newTags.Count < request.TagIds.Count)
            {
                var foundTagIds = newTags.Select(tag => tag.Id).ToHashSet();
                var notFoundTagIds = tagIdsToFetch
                    .Where(id => !foundTagIds.Contains(id))
                    .Select(t => t.Value)
                    .ToList();

                return Errors.Tag.NotFoundSomeIds(notFoundTagIds);
            }
            
            venue.UpdateTags(newTags);
        }
        
        venue.Update(request.Name, request.Description, request.Address);
        await unitOfWork.SaveAsync(cancellationToken);
        
        return new VenueResult(
            venue.Id.Value,
            venue.Name,
            venue.Description,
            venue.Address,
            venue.Tags.ToList().ConvertAll(tag => new TagResult(tag.Id.Value, tag.Name, tag.Description)));
    }
}