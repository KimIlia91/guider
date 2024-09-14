using Guider.Domain.Common.Models;

namespace Guider.Domain.Categories.ValueObjects;

public sealed class CategoryId : ValueObject
{
    public Guid Value { get; }

    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId CreateUnique()
    {
        return new CategoryId(Guid.NewGuid());
    }

    public static CategoryId Convert(Guid id)
    {
        return new CategoryId(id);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}