using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(ApplicationDbContext dbContext) 
    : Repository<Category, CategoryId>(dbContext), ICategoryRepository
{
}