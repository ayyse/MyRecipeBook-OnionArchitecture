namespace MyRecipeBook.Application.Interfaces.Helpers;

public interface ITokenHelper
{
    string GenerateToken(string email);
}