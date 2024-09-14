using Guider.Domain.Common.Models;
using Guider.Domain.Common.Specifications;
using Guider.Infrastructure.Persistence.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Guider.Infrastructure.Persistence.Repositories;

internal abstract class Repository<TEntity, TEntityId>(
    ApplicationDbContext dbContext)
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    private readonly ApplicationDbContext _context = dbContext;
    protected readonly DbSet<TEntity> DbSet = dbContext.Set<TEntity>();
    
    public async Task<List<TEntity>> GetAllAsync(
        Specification<TEntity, TEntityId>? specification = null, 
        CancellationToken cancellationToken = default)
    {
        return await SpecificationExecutor
            .GetQuery(DbSet, specification)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<TEntity?> GetAsync(
        Specification<TEntity, TEntityId> specification, 
        CancellationToken cancellationToken)
    {
        return await SpecificationExecutor
            .GetQuery(DbSet, specification)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(TEntityId id, CancellationToken cancellationToken)
    {
        return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
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