using ErrorOr;
using Guider.Domain.Common.Resources;

namespace Guider.Domain.Common.Errors;

public static partial class Errors
{
    public static class Venue
    {
        public static Error NotFoundById(Guid id) => Error.NotFound(
            code: nameof(id),
            description: string.Format(ErrorResources.VenueNotFoundById, id));

        public static Error NameConflict(string name) => Error.Conflict(
            code: nameof(name),
            description: string.Format(ErrorResources.VenueNameConflict, name));
    }
}