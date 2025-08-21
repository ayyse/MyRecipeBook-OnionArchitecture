namespace MyRecipeBook.Application.Dtos.Recipe;

public class UpdateRecipeDto
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int PreparationTime { get; set; } 
    public int CookingTime { get; set; } 
    public int NumberOfServings { get; set; } 
}