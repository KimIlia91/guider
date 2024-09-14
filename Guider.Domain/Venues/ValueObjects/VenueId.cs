using Guider.Domain.Common.Models;

namespace Guider.Domain.Entities.Venues.ValueObjects;

public sealed class VenueId : ValueObject
{
    public Guid Value { get; }

    private VenueId(Guid value)
    {
        Value = value;
    }

    public static VenueId CreateUnique()
    {
        return new VenueId(Guid.NewGuid());
    }

    public static VenueId Convert(Guid id)
    {
        return new VenueId(id);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }  
}