namespace Guider.Common.Models.Venues;

public sealed record UpdateVenueRequest(
    Guid Id,
    string Name,
    string Address,
    Guid CategoryId,
    string Description = "",
    List<Guid>? TagIds = null);