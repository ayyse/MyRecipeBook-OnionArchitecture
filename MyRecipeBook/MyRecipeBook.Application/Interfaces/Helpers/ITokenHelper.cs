using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Interfaces.Helpers;

public interface ITokenHelper
{
    string GenerateToken(User user);
}