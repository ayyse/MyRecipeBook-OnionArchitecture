using MyRecipeBook.Application.Dtos.User;

namespace MyRecipeBook.Application.Interfaces.AppServices;

public interface IAuthAppService
{
    Task RegisterAsync(RegisterDto input);

    Task<string> LoginAsync(LoginDto input);
}