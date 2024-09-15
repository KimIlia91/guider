using System.ComponentModel.DataAnnotations;
using Guider.Domain.Tags;

namespace Guider.Common.Models.Tags;

/// <summary>
/// Represents a request to create a new tag.
/// </summary>
public sealed class CreateTagRequest
{
    /// <summary>
    /// Gets or sets the name of the tag.
    /// </summary>
    /// <remarks>
    /// The name is required and its maximum length is defined by TagConstants.NameLength(255).
    /// </remarks>
    [Required]
    [MaxLength(TagConstants.NameLength)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the tag.
    /// </summary>
    /// <remarks>
    /// The description is optional and its maximum length is defined by TagConstants.DescriptionLenght(4000).
    /// </remarks>
    [MaxLength(TagConstants.DescriptionLenght)]
    public string Description { get; set; } = string.Empty;
}