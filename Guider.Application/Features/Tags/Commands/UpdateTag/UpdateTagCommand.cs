using Guider.Application.Common.Models;
using Guider.Application.Common.Repositories;
using Guider.Application.Features.Tags.Commands.CreateTag;
using Guider.Domain.Categories;
using Guider.Domain.Tags;
using Guider.Domain.Tags.ValueObjects;
using MediatR;

namespace Guider.Application.Features.Tags.Commands.UpdateTag;

public sealed record UpdateTagCommand(
    Guid Id,
    string Name,
    string Description) : IRequest<TagResult>;

internal sealed class UpdateTagCommandHandler(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateTagCommand, TagResult>
{
    public async Task<TagResult> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tagToUpdate = await tagRepository.GetByIdAsync(TagId.Convert(request.Id), cancellationToken);

        if (tagToUpdate is null)
        {
            throw new ArgumentNullException(nameof(request.Id));
        }
        
        tagToUpdate.Update(request.Name, request.Description);
        await tagRepository.UpdateAsync(tagToUpdate, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        return new TagResult(tagToUpdate.Id.Value, tagToUpdate.Name, tagToUpdate.Description);
    }
}