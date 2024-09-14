using Guider.Domain.Common.Models;
using Guider.Domain.Common.Specifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Guider.Infrastructure.Persistence.Specifications;

public static class SpecificationExecutor
{
    public static IQueryable<TEntity> GetQuery<TEntity, TEntityId>(
        IQueryable<TEntity> inputQueryable,
        Specification<TEntity, TEntityId>? specification = null)
        where TEntity : Entity<TEntityId>
        where TEntityId : class
    {
        var queryable = inputQueryable;

        if (specification is null) return queryable;

        if (specification.Criteria is not null) queryable = queryable.Where(specification.Criteria);
        
        queryable = specification.IncludeExpressions.Aggregate(
            queryable,
            (current, includeExpression) => current.Include(includeExpression));

        if (specification.IsAsNoTracking) queryable = queryable.AsNoTracking();

        return queryable;
    }
}