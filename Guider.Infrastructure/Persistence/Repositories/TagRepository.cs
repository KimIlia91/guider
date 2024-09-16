using Guider.Domain.Tags;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Infrastructure.Persistence.Repositories;

internal sealed class TagRepository(ApplicationDbContext dbContext) 
    : Repository<Tag, TagId>(dbContext), ITagRepository
{
}