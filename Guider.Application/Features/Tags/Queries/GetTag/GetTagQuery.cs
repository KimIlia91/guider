using Guider.Application.Common.Models;
using Guider.Application.Features.Tags.Commands.CreateTag;
using Guider.Domain.Categories;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using Guider.Domain.Tags.ValueObjects;
using MediatR;

namespace Guider.Application.Features.Tags.Queries.GetTag;

public sealed record GetTagQuery(Guid Id) : IRequest<TagResult>;

internal sealed class GetTagQueryHandler(
    ITagRepository tagRepository) : IRequestHandler<GetTagQuery, TagResult>
{
    public async Task<TagResult> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await tagRepository
            .GetAsync(new GetTagByIdNoTrackingSpecification(TagId.Convert(request.Id)), cancellationToken);

        if (tag is null)
        {
            throw new ArgumentNullException(nameof(request.Id));
        }

        return new TagResult(tag.Id.Value, tag.Name, tag.Description);
    }
}