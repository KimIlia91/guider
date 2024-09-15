using ErrorOr;
using Guider.Domain.Common.Resources;
using Guider.Domain.Tags.ValueObjects;

namespace Guider.Domain.Common.Errors;

public static partial class Errors
{
    public static class Tag
    {
        public static Error NotFoundById(Guid id) => Error.NotFound(
            code: nameof(id),
            description: string.Format(ErrorResources.TagNotFoundById, id));

        public static Error NotFoundSomeIds(List<Guid> tagIds) => Error.NotFound(
            code: nameof(tagIds),
            description: string.Format(ErrorResources.TagNotFoundSomeIds, string.Join(", ", tagIds)));

        public static Error NameConflict(string name) => Error.Validation(
            code: nameof(name),
            description: string.Format(ErrorResources.TagNameConflict, name));
    }
}