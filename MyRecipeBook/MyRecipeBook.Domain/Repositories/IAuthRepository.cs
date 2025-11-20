using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Domain.Repositories;

public interface IAuthRepository : IRepository<User>
{
    Task<User> GetByEmailAsync(string email);
}