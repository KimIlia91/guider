using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;

namespace Guider.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext dbContext) 
    : Repository<Category, CategoryId>(dbContext), ICategoryRepository
{
}