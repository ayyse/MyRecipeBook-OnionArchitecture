using MyRecipeBook.Application.Dtos.Category;

namespace MyRecipeBook.Application.Interfaces.AppServices;

public interface ICategoryAppService
{
    Task<List<CategoryDto>> GetAllAsync();
    Task<List<CategoryDto>> GetParentCategoriesAsync();
    Task<CategoryDto> GetByIdAsync(Guid id);
    Task<CategoryDto> CreateAsync(CreateCategoryDto input);
    Task<CategoryDto> UpdateAsync(Guid id, UpdateCategoryDto input);
    Task DeleteAsync(Guid id);
}