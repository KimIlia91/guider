namespace Guider.Common.Models.Tags;

public sealed record UpdateTagRequest(
    Guid Id,
    string Name,
    string Description = "");