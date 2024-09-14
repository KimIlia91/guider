namespace Guider.Common.Models.Tags;

public sealed record CreateTagRequest(
    string Name,
    string Description = "");