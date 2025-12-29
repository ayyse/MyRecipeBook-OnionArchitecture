using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Infrastructure.DbContexts;

namespace MyRecipeBook.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    private readonly MyRecipeBookDbContext _myRecipeBookDbContext;
    public CategoryRepository(MyRecipeBookDbContext myRecipeBookDbContext) : base(myRecipeBookDbContext)
    {
        _myRecipeBookDbContext = myRecipeBookDbContext;
    }

    public Task<List<Category>> GetParentCategoriesAsync()
    {
        var parentCategories = _myRecipeBookDbContext.Categories
            .Where(x => x.ParentCategoryId == null)
            .Include(x => x.SubCategories)
            .ToListAsync();
        return parentCategories;
    }
}