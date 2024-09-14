namespace Guider.Common.Models.Venues;

public sealed record CreateVenueRequest(
    string Name,
    string Address,
    Guid CategoryId,
    string Description = "",
    List<Guid>? TagIds = null)
{
    public List<Guid> TagIds { get; init; } = TagIds ?? new List<Guid>();
}