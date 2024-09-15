using ErrorOr;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Domain.Common.Errors;

public static partial class Errors
{
    public static class Tag
    {
        public static Error NotFoundById(Guid id) => Error.NotFound(
            code: nameof(id),
            description: $"Tag not found by id: {id}");

        public static Error NotFoundSomeIds(List<Guid> tagIds) => Error.NotFound(
            code: nameof(tagIds),
            description: $"Some tag IDs not found: {string.Join(", ", tagIds)}");

        public static Error NameConflict(string name) => Error.Validation(
            code: nameof(name),
            description: $"Tag name already exist: {name}");
    }
}