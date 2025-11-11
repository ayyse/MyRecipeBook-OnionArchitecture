using System.ComponentModel.DataAnnotations;

namespace MyRecipeBook.Application.Dtos.Recipe;

public class CreateRecipeDto
{
    [Required(ErrorMessage = "Recipe name is required")]
    [MaxLength(100, ErrorMessage = "Recipe name max length can be 100 characters.")]
    public string Name { get; set; }
    
    [MaxLength(300, ErrorMessage = "Description max length can be 300 characters")]
    public string? Description { get; set; }
    
    [Required(ErrorMessage = "Preparation time is required")]
    [Range(1, 20, ErrorMessage = "Preparation time must be between 1 and 180 minutes.")]
    public int PreparationTime { get; set; } 
    
    [Required(ErrorMessage = "Cooking time is required")]
    [Range(1, 20, ErrorMessage = "Cooking time must be between 1 and 360 minutes.")]
    public int CookingTime { get; set; } 
    
    [Required(ErrorMessage = "Number of servings is required")]
    [Range(1, 20, ErrorMessage = "The number of servings must be between 1 and 20.")]
    public int NumberOfServings { get; set; } 
}