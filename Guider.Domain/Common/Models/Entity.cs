using Guider.Domain.Common.Primitives;

namespace Guider.Domain.Common.Models;

public abstract class Entity<TId>(TId id) : ISoftDeleted, IHaveName, IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; } = id;

    public string Name { get; protected set; } = string.Empty;

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; protected set; } = DateTime.UtcNow;

    public bool IsDeleted { get; protected set; } = false;

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId> entity && Id.Equals(entity.Id);
    }

    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public static bool operator ==(Entity<TId> left, Entity<TId> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId> left, Entity<TId> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

}