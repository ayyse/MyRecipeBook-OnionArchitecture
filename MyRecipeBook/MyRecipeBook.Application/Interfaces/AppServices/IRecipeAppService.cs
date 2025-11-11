using MyRecipeBook.Application.Dtos.Recipe;

namespace MyRecipeBook.Application.Interfaces.AppServices;

public interface IRecipeAppService
{
    Task<List<RecipeDto>> GetAllAsync();
    Task<RecipeDto> GetByIdAsync(Guid id);
    Task<RecipeDto> CreateAsync(CreateRecipeDto input);
    Task<RecipeDto> UpdateAsync(Guid id, UpdateRecipeDto input);
    Task DeleteAsync(Guid id);
}