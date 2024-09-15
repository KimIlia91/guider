using System.ComponentModel.DataAnnotations;
using Guider.Domain.Tags;

namespace Guider.Common.Models.Tags;

/// <summary>
/// Represents a request to update an existing tag.
/// </summary>
public sealed class UpdateTagRequest
{
    /// <summary>
    /// Gets or sets the unique identifier for the tag.
    /// </summary>
    /// <remarks>
    /// This property is required and is used to uniquely identify a tag that is to be updated.
    /// </remarks>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the tag.
    /// </summary>
    /// <remarks>
    /// The name is a required property and has a maximum length defined by TagConstants.NameLength(255).
    /// </remarks>
    [Required]
    [MaxLength(TagConstants.NameLength)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the description of the tag.
    /// </summary>
    /// <remarks>
    /// Provides additional information about the tag.
    /// This field is optional and can have a maximum length defined by TagConstants.DescriptionLength(4000).
    /// </remarks>
    [MaxLength(TagConstants.DescriptionLenght)]
    public string Description { get; set; } = string.Empty;
}