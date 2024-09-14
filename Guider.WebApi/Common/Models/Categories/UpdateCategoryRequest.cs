namespace Guider.Common.Models.Categories;

public sealed record UpdateCategoryRequest(
    Guid Id,
    string Name,
    string Description);