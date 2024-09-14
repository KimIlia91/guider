using System.Linq.Expressions;
using Guider.Domain.Common.Models;

namespace Guider.Domain.Common.Specifications;

public abstract class Specification<TEntity, TEntityId>(Expression<Func<TEntity, bool>>? criteria)
    where TEntity : Entity<TEntityId>
    where TEntityId : class
{
    public bool IsAsNoTracking { get; protected set; }
    
    public Expression<Func<TEntity, bool>>? Criteria { get; } = criteria;

    public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];
    
    protected void AddInclude(Expression<Func<TEntity, object>> includeExpression) =>
        IncludeExpressions.Add(includeExpression);
}