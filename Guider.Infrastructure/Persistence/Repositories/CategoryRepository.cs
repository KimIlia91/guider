using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Specifications;

namespace Guider.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository : ICategoryRepository
{
    public Task<Category?> GetByIdAsync(CategoryId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetAllAsync(
        Specification<Category, CategoryId>? specification = null, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task CreateAsync(Category category, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ExistByIdAsync(CategoryId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}