using ErrorOr;
using Guider.Application.Common;
using Guider.Application.Common.Models;
using Guider.Domain.Common.Errors;
using Guider.Domain.Tags;
using MediatR;

namespace Guider.Application.Features.Tags.Commands.Create;

public sealed record CreateTagCommand(string Name, string Description = "") 
    : IRequest<ErrorOr<TagResult>>;

public sealed class CreateTagCommandHandler(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateTagCommand, ErrorOr<TagResult>>
{
    public async Task<ErrorOr<TagResult>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        if (await tagRepository.ExistByNameAsync(request.Name, cancellationToken))
            return Errors.Tag.NameConflict(request.Name);
        
        var newTag = Tag.Create(request.Name, request.Description);
        await tagRepository.CreateAsync(newTag, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        return new TagResult(newTag.Id.Value, newTag.Name, newTag.Description);
    }
}