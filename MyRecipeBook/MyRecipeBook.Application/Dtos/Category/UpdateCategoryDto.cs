namespace MyRecipeBook.Application.Dtos.Category;

public class UpdateCategoryDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public Guid? ParentCategoryId { get; set; }
}