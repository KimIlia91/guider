using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext dbContext) 
    : Repository<Category, CategoryId>(dbContext), ICategoryRepository
{
    public async Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await DbSet
            .AnyAsync(e => e.Name.ToLower().Equals(name.ToLower()), cancellationToken);
    }
}