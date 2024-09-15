using ErrorOr;

namespace Guider.Domain.Common.Errors;

public static partial class Errors
{
    public static class Category
    {
        public static Error NotFoundById(Guid id) => Error.NotFound(
            code: nameof(id),
            description: $"Category not found by id: {id}");

        public static Error NameConflict(string name) => Error.Conflict(
            code: nameof(name),
            description: $"Category name already exist: {name}");
    }
}