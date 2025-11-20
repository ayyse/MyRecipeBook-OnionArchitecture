using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;
using MyRecipeBook.Infrastructure.DbContexts;

namespace MyRecipeBook.Infrastructure.Repositories;

public class AuthRepository : Repository<User>, IAuthRepository
{
    public AuthRepository(MyRecipeBookDbContext myRecipeBookDbContext) : base(myRecipeBookDbContext)
    {
    }

    public async Task<User> GetByEmailAsync(string email)
    {
        var user = await _dbSet.FirstOrDefaultAsync(x => x.Email == email);
        return user;
    }
}