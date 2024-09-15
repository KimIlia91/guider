using ErrorOr;
using Guider.Application.Common;
using Guider.Domain.Common.Errors;
using Guider.Domain.Tags;
using Guider.Domain.Tags.ValueObjects;
using MediatR;

namespace Guider.Application.Features.Tags.Commands.Delete;

public sealed record DeleteTagCommand(Guid Id) : IRequest<ErrorOr<TagId>>;

internal sealed class DeleteTagCommandHandler(
    ITagRepository tagRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteTagCommand, ErrorOr<TagId>>
{
    public async Task<ErrorOr<TagId>> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository.GetByIdAsync(TagId.Convert(request.Id), cancellationToken);

        if (tag is null)
            return Errors.Tag.NotFoundById(request.Id);
        
        tag.Delete();
        tagRepository.Update(tag);
        await unitOfWork.SaveAsync(cancellationToken);
        return tag.Id;
    }
}