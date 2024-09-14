using Guider.Domain.Common.Models;
using Guider.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence.Repositories;

internal abstract class Repository<TEntity, TEntityId>(
    ApplicationDbContext dbContext)
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();
    
    public Task<List<TEntity>> GetAllAsync(
        Specification<TEntity, TEntityId>? specification = null, 
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
    
    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken)
    {
        return await DbSet
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken: cancellationToken);
    }
    
    public async Task<bool> ExistByIdAsync(TEntityId id, CancellationToken cancellationToken)
    {
        return await DbSet.AnyAsync(e => e.Id.Equals(id), cancellationToken);
    }
    
    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async Task CreateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
    }
}