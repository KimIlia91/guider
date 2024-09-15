using ErrorOr;

namespace Guider.Domain.Common.Errors;

public static partial class Errors
{
    public static class Venue
    {
        public static Error NotFoundById(Guid id) => Error.NotFound(
            code: nameof(id),
            description: $"Venue not found by id: {id}");

        public static Error NameConflict(string name) => Error.Conflict(
            code: nameof(name),
            description: $"Venue name already exist: {name}");
    }
}