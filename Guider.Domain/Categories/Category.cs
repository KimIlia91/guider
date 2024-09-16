using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Models;
using Guider.Domain.Venues;

namespace Guider.Domain.Categories;

public sealed class Category : Entity<CategoryId>
{
    private readonly List<Venue> _venues = [];

    public string Description { get; private set; } = string.Empty;

    public IReadOnlyCollection<Venue> Venues => _venues.AsReadOnly();
    
    private Category() : base(CategoryId.CreateUnique())
    {
    }

    private Category(string name, string description) : base(CategoryId.CreateUnique())
    {
        Name = name;
        Description = description;
    }

    public static Category Create(string name, string description)
    {
        return new Category(name, description);
    }

    public void Update(string name, string description)
    {
        Name = name.Trim();
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
        UpdatedAt = DateTime.UtcNow;
    }
}