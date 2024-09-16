using Guider.Domain.Common.Models;
using Guider.Domain.Entities.Venues;
using Guider.Domain.Tags.ValueObjects;
using Guider.Domain.Venues;

namespace Guider.Domain.Tags;

public sealed class Tag : Entity<TagId>
{
    private readonly List<Venue> _venues = [];

    public string Description { get; private set; } = string.Empty;

    public IReadOnlyCollection<Venue> Venues => _venues.AsReadOnly();
    
    private Tag() : base(TagId.CreateUnique())
    {
    }

    private Tag(string name, string description) : base(TagId.CreateUnique())
    {
        Name = name;
        Description = description;
    }

    public static Tag Create(string name, string description)
    {
        return new Tag(name, description);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddVenues(List<Venue> venues)
    {
        _venues.AddRange(venues);
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;;
    }
}