using System.ComponentModel.DataAnnotations;
using Guider.Domain.Venues;

namespace Guider.Common.Models.Venues;

/// <summary>
/// 
/// </summary>
public class CreateVenueRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(VenueConstants.NameMaxLength)]
    public required string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(VenueConstants.AddressMaxLenght)]
    public required string Address { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Required]
    public required Guid CategoryId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(VenueConstants.DescriptionMaxLenght)]
    public string Description { get; set; } = "";

    /// <summary>
    /// 
    /// </summary>
    public List<Guid>? TagIds { get; set; } = null;
}