using System.ComponentModel.DataAnnotations;
using Guider.Domain.Tags;

namespace Guider.Common.Models.Tags;

/// <summary>
/// 
/// </summary>
public sealed class UpdateTagRequest
{
    /// <summary>
    /// 
    /// </summary>
    [Required]
    public required Guid Id { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [MaxLength(TagConstants.NameLength)]
    public required string Name { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    [MaxLength(TagConstants.DescriptionLenght)]
    public string Description { get; set; } = string.Empty;
}