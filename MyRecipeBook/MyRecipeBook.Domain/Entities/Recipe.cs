namespace MyRecipeBook.Domain.Entities;

public class Recipe : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public int PreparationTime { get; set; }  // minutes
    public int CookingTime { get; set; } // minutes
    public int NumberOfServings { get; set; } 
}