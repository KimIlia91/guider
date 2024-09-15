using ErrorOr;
using Guider.Application.Common.Models;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using Guider.Domain.Tags.ValueObjects;
using MediatR;
using Errors = Guider.Domain.Common.Errors.Errors;

namespace Guider.Application.Features.Tags.Queries.GetAll;

public sealed record GetTagQuery(Guid Id) : IRequest<ErrorOr<TagResult>>;

internal sealed class GetTagQueryHandler(
    ITagRepository tagRepository) : IRequestHandler<GetTagQuery, ErrorOr<TagResult>>
{
    public async Task<ErrorOr<TagResult>> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository
            .GetAsync(new GetTagByIdNoTrackingSpecification(TagId.Convert(request.Id)), cancellationToken);

        if (tag is null)
            return Errors.Tag.NotFoundById(request.Id);

        return new TagResult(tag.Id.Value, tag.Name, tag.Description);
    }
}