using System.ComponentModel.DataAnnotations;
using Guider.Domain.Venues;

namespace Guider.Common.Models.Venues;

/// <summary>
/// Represents a request to create a new venue with necessary details.
/// </summary>
public class CreateVenueRequest
{
    /// <summary>
    /// Gets or sets the name of the venue.
    /// </summary>
    /// <remarks>
    /// This property is required and its maximum length is defined by VenueConstants.NameMaxLength(255).
    /// </remarks>
    [Required]
    [MaxLength(VenueConstants.NameMaxLength)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the address of the venue.
    /// </summary>
    /// <remarks>
    /// This property is required and its maximum length is defined by VenueConstants.AddressMaxLenght(4000).
    /// </remarks>
    [Required]
    [MaxLength(VenueConstants.AddressMaxLenght)]
    public required string Address { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the category associated with the venue.
    /// </summary>
    /// <remarks>
    /// This property is required to categorize a venue appropriately.
    /// </remarks>
    [Required]
    public required Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the description of the venue.
    /// </summary>
    /// <remarks>
    /// This property is optional and its maximum length is defined by VenueConstants.DescriptionMaxLenght(4000).
    /// </remarks>
    [MaxLength(VenueConstants.DescriptionMaxLenght)]
    public string Description { get; set; } = "";

    /// <summary>
    /// Gets or sets the list of tag IDs associated with the venue.
    /// </summary>
    /// <remarks>
    /// This property is optional and may be null. It allows categorization or additional metadata tagging of the venue.
    /// </remarks>
    public List<Guid>? TagIds { get; set; } = null;
}