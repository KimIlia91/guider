using Guider.Application.Common.Models;
using Guider.Application.Common.Repositories;
using Guider.Domain.Categories;
using Guider.Domain.Tags;
using MediatR;

namespace Guider.Application.Features.Tags.Commands.CreateTag;

public sealed record CreateTagCommand(string Name, string Description = "") : IRequest<TagResult>;

public sealed class CreateTagCommandHandler(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateTagCommand, TagResult>
{
    public async Task<TagResult> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        if (await tagRepository.ExistTagByNameAsync(request.Name, cancellationToken))
        {
            throw new ArgumentException(nameof(request.Name));
        }
        
        var newTag = Tag.Create(request.Name, request.Description);
        await tagRepository.CreateAsync(newTag, cancellationToken);
        await unitOfWork.SaveAsync(cancellationToken);
        return new TagResult(newTag.Id.Value, newTag.Name, newTag.Description);
    }
}