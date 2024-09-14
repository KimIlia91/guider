using Guider.Application.Common.Models;

namespace Guider.Application.Features.Venues.Models;

public sealed record VenueResult(
    Guid Id,
    string Name,
    string Description,
    string Address,
    List<TagResult> Tags);