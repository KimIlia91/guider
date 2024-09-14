using Guider.Application.Common;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using Guider.Domain.Tags.ValueObjects;
using MediatR;

namespace Guider.Application.Features.Tags.Commands.DeleteTag;

public sealed record DeleteTagCommand(Guid Id) : IRequest;

internal sealed class DeleteTagCommandHandler(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteTagCommand>
{
    public async Task Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(TagId.Convert(request.Id), cancellationToken);

        if (tag is null)
            throw new ArgumentException(nameof(request.Id));
        
        tag.Delete();
        tagRepository.Update(tag);
        await unitOfWork.SaveAsync(cancellationToken);
    }
}