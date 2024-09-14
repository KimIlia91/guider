using Guider.Application.Common;

namespace Guider.Infrastructure.Persistence.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}