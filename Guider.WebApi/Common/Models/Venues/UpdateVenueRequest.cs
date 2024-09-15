using System.ComponentModel.DataAnnotations;
using Guider.Domain.Venues;

namespace Guider.Common.Models.Venues;

/// <summary>
/// Represents a request to update an existing venue.
/// </summary>
public sealed record UpdateVenueRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public required Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the venue.
    /// </summary>
    /// <remarks>
    /// The name is required and has a maximum length defined by VenueConstants.NameMaxLength(255).
    /// </remarks>
    [Required]
    [MaxLength(VenueConstants.NameMaxLength)]
    public required string Name { get; set; }

    /// <summary>
    /// Gets or sets the address of the venue.
    /// </summary>
    /// <remarks>
    /// The address is required and has a maximum length defined by VenueConstants.AddressMaxLenght(4000).
    /// </remarks>
    [Required]
    [MaxLength(VenueConstants.AddressMaxLenght)]
    public required string Address { get; set; }

    /// <summary>
    /// Gets or sets the ID of the category associated with the venue.
    /// </summary>
    /// <remarks>
    /// The CategoryId is required to categorize the venue appropriately in the system.
    /// </remarks>
    [Required]
    public required Guid CategoryId { get; set; }

    /// <summary>
    /// Gets or sets the description of the venue.
    /// </summary>
    /// <remarks>
    /// The description can be up to a maximum length defined by VenueConstants.DescriptionMaxLength(4000).
    /// </remarks>
    [MaxLength(VenueConstants.DescriptionMaxLenght)]
    public string Description { get; set; } = "";

    /// <summary>
    /// Gets or sets the list of tag IDs associated with the venue.
    /// </summary>
    /// <remarks>
    /// Tag IDs are optional and allow categorization or labeling of the venue with multiple tags.
    /// </remarks>
    public List<Guid>? TagIds { get; set; } = null;
}