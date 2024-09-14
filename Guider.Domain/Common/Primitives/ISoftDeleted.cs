namespace Guider.Domain.Common.Primitives;

public interface ISoftDeleted
{
    bool IsDeleted { get; }
}