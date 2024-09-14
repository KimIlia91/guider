using Guider.Domain.Tags;
using Guider.Domain.Tags.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence.Repositories;

internal sealed class TagRepository(ApplicationDbContext dbContext) 
    : Repository<Tag, TagId>(dbContext), ITagRepository
{
    public async Task<bool> ExistByNameAsync(string tagName, CancellationToken cancellationToken)
    {
        return await DbSet
            .AnyAsync(t => t.Name.ToLower().Equals(tagName.ToLower()), cancellationToken);
    }
}