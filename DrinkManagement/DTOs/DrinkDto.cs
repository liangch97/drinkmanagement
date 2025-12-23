using System.ComponentModel.DataAnnotations;

namespace DrinkManagement.DTOs;

public class DrinkDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateDrinkDto
{
    [Required(ErrorMessage = "饮品名称不能为空")]
    [StringLength(100, ErrorMessage = "饮品名称长度不能超过100个字符")]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500, ErrorMessage = "描述长度不能超过500个字符")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "价格不能为空")]
    [Range(0.01, 9999.99, ErrorMessage = "价格必须在0.01到9999.99之间")]
    public decimal Price { get; set; }
    
    [Required(ErrorMessage = "分类ID不能为空")]
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "库存不能为空")]
    [Range(0, int.MaxValue, ErrorMessage = "库存不能为负数")]
    public int Stock { get; set; }
    
    [StringLength(500, ErrorMessage = "图片URL长度不能超过500个字符")]
    public string? ImageUrl { get; set; }
}

public class UpdateDrinkDto
{
    [StringLength(100, ErrorMessage = "饮品名称长度不能超过100个字符")]
    public string? Name { get; set; }
    
    [StringLength(500, ErrorMessage = "描述长度不能超过500个字符")]
    public string? Description { get; set; }
    
    [Range(0.01, 9999.99, ErrorMessage = "价格必须在0.01到9999.99之间")]
    public decimal? Price { get; set; }
    
    public int? CategoryId { get; set; }
    
    [Range(0, int.MaxValue, ErrorMessage = "库存不能为负数")]
    public int? Stock { get; set; }
    
    [StringLength(500, ErrorMessage = "图片URL长度不能超过500个字符")]
    public string? ImageUrl { get; set; }
}
