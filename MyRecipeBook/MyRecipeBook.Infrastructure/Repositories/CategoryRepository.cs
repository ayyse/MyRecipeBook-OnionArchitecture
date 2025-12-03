using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Infrastructure.DbContexts;

namespace MyRecipeBook.Infrastructure.Repositories;

public class CategoryRepository : Repository<Category>, ICategoryRepository
{
    public CategoryRepository(MyRecipeBookDbContext myRecipeBookDbContext) : base(myRecipeBookDbContext)
    {
    }
}