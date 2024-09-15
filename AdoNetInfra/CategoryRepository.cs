using Guider.Domain.Categories;
using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Specifications;

namespace AdoNetInfra;

public class CategoryRepository : ICategoryRepository
{
    public Task<Category?> GetAsync(Specification<Category, CategoryId> specification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Category?> GetByIdAsync(CategoryId id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<List<Category>> GetAllAsync(Specification<Category, CategoryId>? specification = null, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(Category category)
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