using Guider.Domain.Common.Specifications;
using Guider.Domain.Tags;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Infrastructure.Persistence.Repositories;

internal sealed class TagRepository : ITagRepository
{
    public Task<Tag?> GetByIdAsync(TagId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistTagByNameAsync(string tagName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Tag tag, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Tag tag, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Tag>> GetAllAsync(Specification<Tag, TagId>? specification = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}