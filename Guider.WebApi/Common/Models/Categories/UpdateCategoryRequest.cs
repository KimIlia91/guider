using System.ComponentModel.DataAnnotations;
using Guider.Domain.Categories;

namespace Guider.Common.Models.Categories;

/// <summary>
/// Represents the request payload for updating a category.
/// </summary>
public sealed class UpdateCategoryRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the category.
    /// </summary>
    /// <remarks>
    /// This identifier is required and must follow the GUID format.
    /// </remarks>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    /// <remarks>
    /// This field is required and must not exceed the length defined by CategoryConstants.NameMaxLength(255).
    /// </remarks>
    [Required]
    [MaxLength(CategoryConstants.NameMaxLength)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the category.
    /// </summary>
    /// <remarks>
    /// This field must not exceed the length defined by CategoryConstants.DescriptionMaxLenght(4000).
    /// </remarks>
    [MaxLength(CategoryConstants.DescriptionMaxLenght)]
    public string Description { get; set; } = string.Empty;
}