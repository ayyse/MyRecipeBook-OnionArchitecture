using MyRecipeBook.Application.Common;

namespace MyRecipeBook.Application.Dtos.Category;

public class CategoryDto : BaseDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
    public List<CategoryDto>? SubCategories { get; set; }
}