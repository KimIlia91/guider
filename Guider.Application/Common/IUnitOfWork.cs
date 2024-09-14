namespace Guider.Application.Common.Repositories;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken);
}