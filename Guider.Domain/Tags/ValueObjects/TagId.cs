using Guider.Domain.Common.Models;

namespace Guider.Domain.Tags.ValueObjects;

public sealed class TagId : ValueObject
{
    public Guid Value { get; }

    private TagId(Guid value)
    {
        Value = value;
    }

    public static TagId CreateUnique()
    {
        return new TagId(Guid.NewGuid());
    }

    public static TagId Convert(Guid id)
    {
        return new TagId(id);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    } 
}