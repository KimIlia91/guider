namespace Guider.Application.Common;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken);
}