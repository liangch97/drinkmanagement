using System.ComponentModel.DataAnnotations;

namespace DrinkManagement.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public int DrinkCount { get; set; }
}

public class CreateCategoryDto
{
    [Required(ErrorMessage = "分类名称不能为空")]
    [StringLength(50, ErrorMessage = "分类名称长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(200, ErrorMessage = "描述长度不能超过200个字符")]
    public string? Description { get; set; }
}

public class UpdateCategoryDto
{
    [StringLength(50, ErrorMessage = "分类名称长度不能超过50个字符")]
    public string? Name { get; set; }
    
    [StringLength(200, ErrorMessage = "描述长度不能超过200个字符")]
    public string? Description { get; set; }
}
