using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Specifications;

namespace Guider.Domain.Categories;

public interface ICategoryRepository
{
    Task<Category?> GetByIdAsync(CategoryId id, CancellationToken cancellationToken);

    Task<List<Category>> GetAllAsync(
        Specification<Category, CategoryId>? specification = null,
        CancellationToken cancellationToken = default);

    void Update(Category category);

    Task CreateAsync(Category category, CancellationToken cancellationToken);

    Task<bool> ExistByNameAsync(string name, CancellationToken cancellationToken);

    Task<bool> ExistByIdAsync(CategoryId id, CancellationToken cancellationToken);
}