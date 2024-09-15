using ErrorOr;
using Guider.Application.Common;
using Guider.Application.Common.Models;
using Guider.Domain.Tags;
using Guider.Domain.Tags.ValueObjects;
using MediatR;
using Errors = Guider.Domain.Common.Errors.Errors;

namespace Guider.Application.Features.Tags.Commands.Update;

public sealed record UpdateTagCommand(
    Guid Id,
    string Name,
    string Description) : IRequest<ErrorOr<TagResult>>;

internal sealed class UpdateTagCommandHandler(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateTagCommand, ErrorOr<TagResult>>
{
    public async Task<ErrorOr<TagResult>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tagToUpdate = await tagRepository
            .GetByIdAsync(TagId.Convert(request.Id), cancellationToken);

        if (tagToUpdate is null)
            return Errors.Tag.NotFoundById(request.Id);
        
        tagToUpdate.Update(request.Name, request.Description);
        tagRepository.Update(tagToUpdate);
        await unitOfWork.SaveAsync(cancellationToken);
        return new TagResult(tagToUpdate.Id.Value, tagToUpdate.Name, tagToUpdate.Description);
    }
}