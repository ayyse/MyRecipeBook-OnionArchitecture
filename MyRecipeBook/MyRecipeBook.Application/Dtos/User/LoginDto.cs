using MyRecipeBook.Application.Common;

namespace MyRecipeBook.Application.Dtos.User;

public class LoginDto : BaseDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}