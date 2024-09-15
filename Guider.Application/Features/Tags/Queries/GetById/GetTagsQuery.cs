using Guider.Application.Common.Models;
using Guider.Domain.Tags;
using Guider.Domain.Tags.Specifications;
using MediatR;

namespace Guider.Application.Features.Tags.Queries.GetById;

public sealed record GetTagsQuery : IRequest<List<TagResult>>;

internal sealed class GetTagsQueryHandler(
    ITagRepository tagRepository) : IRequestHandler<GetTagsQuery, List<TagResult>>
{
    public async Task<List<TagResult>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await tagRepository
            .GetAllAsync(new GetTagsNoTrackingSpecification(), cancellationToken: cancellationToken);
        
        return tags.ConvertAll(tag => new TagResult(tag.Id.Value, tag.Name, tag.Description));
    }
}