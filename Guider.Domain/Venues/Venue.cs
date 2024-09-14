using Guider.Domain.Categories.ValueObjects;
using Guider.Domain.Common.Models;
using Guider.Domain.Entities.Venues.ValueObjects;
using Guider.Domain.Tags;

namespace Guider.Domain.Venues;

public sealed class Venue : Entity<VenueId>
{
    private readonly List<Tag> _tags = [];
    
    public string Name { get; private set; }

    public CategoryId CategoryId { get; private set; }

    public string Address { get; private set; }

    public string Description { get; private set; } = string.Empty;

    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
    
    private Venue() : base(VenueId.CreateUnique())
    {
    }

    private Venue(
        string name, 
        CategoryId categoryId, 
        string address, 
        string description) : base(VenueId.CreateUnique())
    {
        Name = name;
        CategoryId = categoryId;
        Address = address;
        Description = description;
    }

    public static Venue Create(string name, CategoryId categoryId, string address, string description)
    {
        return new Venue(name, categoryId, address, description);
    }

    public void Update(string name, string description, string address)
    {
        Name = name;
        Description = description;
        Address = address;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateCategory(CategoryId categoryId)
    {
        CategoryId = categoryId;
    }
    
    public void UpdateTags(List<Tag> newTags)
    {
        var tagsToRemove = _tags.Where(t => !newTags.Contains(t)).ToList();
        
        foreach (var tag in tagsToRemove)
        {
            _tags.Remove(tag);
        }
        
        var tagsToAdd = newTags.Where(t => !_tags.Contains(t)).ToList();
        _tags.AddRange(tagsToAdd);
    }

    public void AddTags(List<Tag> tags)
    {
        _tags.AddRange(tags);
    }

    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}