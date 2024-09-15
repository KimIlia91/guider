
using System.ComponentModel.DataAnnotations;
using Guider.Domain.Categories;

namespace Guider.Common.Models.Categories;

/// <summary>
/// Represents a request to create a new category.
/// </summary>
public sealed class CreateCategoryRequest
{
    /// <summary>
    /// Gets or sets the name of the category.
    /// </summary>
    /// <remarks>
    /// The name has a maximum length as defined by CategoryConstants.NameMaxLength(255).
    /// </remarks>
    [Required]
    [MaxLength(CategoryConstants.NameMaxLength)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the category.
    /// </summary>
    /// <remarks>
    /// The description has a maximum length as defined by CategoryConstants.DescriptionMaxLength(4000).
    /// </remarks>
    [MaxLength(CategoryConstants.DescriptionMaxLenght)]
    public string Description { get; set; } = string.Empty;
}