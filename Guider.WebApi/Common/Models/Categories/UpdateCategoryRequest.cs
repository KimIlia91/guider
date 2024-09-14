using System.ComponentModel.DataAnnotations;
using Guider.Domain.Categories;

namespace Guider.Common.Models.Categories;

/// <summary>
/// 
/// </summary>
public sealed class UpdateCategoryRequest
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
    [MaxLength(CategoryConstants.NameMaxLength)]
    public required string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [MaxLength(CategoryConstants.DescriptionMaxLenght)]
    public string Description { get; set; } = string.Empty;
}