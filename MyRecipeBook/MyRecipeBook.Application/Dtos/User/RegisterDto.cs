using MyRecipeBook.Application.Common;

namespace MyRecipeBook.Application.Dtos.User;

public class RegisterDto : BaseDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}