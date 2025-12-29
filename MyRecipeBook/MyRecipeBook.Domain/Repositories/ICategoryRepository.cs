using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<List<Category>> GetParentCategoriesAsync();
}