using MyRecipeBook.Application.Repositories;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Infrastructure.DbContexts;

namespace MyRecipeBook.Infrastructure.Repositories;

public class RecipeRepository : Repository<Recipe>, IRecipeRepository
{
    public RecipeRepository(MyRecipeBookDbContext myRecipeBookDbContext) : base(myRecipeBookDbContext)
    {
    }
}