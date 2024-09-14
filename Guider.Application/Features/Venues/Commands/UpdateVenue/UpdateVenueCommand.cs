using Guider.Application.Common;
using Guider.Application.Common.Models;
using Guider.Application.Features.Tags.Commands.CreateTag;
using Guider.Application.Features.Venues.Commands.CreateVenue;
using Guider.Application.Features.Venues.Models;
using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using Guider.Domain.Tags.ValueObjects;
using Guider.Domain.Venues;
using Guider.Domain.Venues.Specifications;
using MediatR;

namespace Guider.Application.Features.Venues.Commands.UpdateVenue;

public sealed record UpdateVenueCommand(
    Guid Id, 
    string Name, 
    string Description, 
    string Address, 
    Guid CategoryId, 
    List<Guid> TagIds) : IRequest<VenueResult>;

internal sealed class UpdateVenueCommandHandler(
    IVenueRepository venueRepository,
    ICategoryRepository categoryRepository,
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateVenueCommand, VenueResult>
{
    public async Task<VenueResult> Handle(UpdateVenueCommand request, CancellationToken cancellationToken)
    {
        var venue = await venueRepository
            .GetAsync(new GetByIdWithTagsSpecification(VenueId.Convert(request.Id)), cancellationToken);

        if (venue is null)
            throw new ArgumentException(nameof(request.Id));

        var categoryId = CategoryId.Convert(request.CategoryId);

        if (venue.CategoryId != categoryId)
        {
            var categoryExist = await categoryRepository.ExistByIdAsync(categoryId, cancellationToken);

            if (!categoryExist)
                throw new ArgumentException(nameof(request.CategoryId));
            
            venue.UpdateCategory(categoryId);
        }

        if (request.TagIds.Count != 0)
        {
            var newTags = await tagRepository
                .GetAllAsync(new GetTagsByIdsSpecification(request.TagIds.ConvertAll(TagId.Convert)), cancellationToken);

            if (newTags.Count < request.TagIds.Count)
                throw new ArgumentException(nameof(request.TagIds));
            
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