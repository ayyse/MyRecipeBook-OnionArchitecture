using MyRecipeBook.Application.Dtos.User;
using MyRecipeBook.Application.Interfaces.AppServices;
using MyRecipeBook.Application.Interfaces.Helpers;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Domain.Repositories;

namespace MyRecipeBook.Application.AppServices;

public class AuthAppService : IAuthAppService
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly IPasswordHelper _passwordHelper;

    public AuthAppService(
        IAuthRepository authRepository, 
        ITokenHelper tokenHelper,
        IPasswordHelper passwordHelper)
    {
        _authRepository = authRepository;
        _tokenHelper = tokenHelper;
        _passwordHelper = passwordHelper;
    }
    
    public async Task RegisterAsync(RegisterDto input)
    {
        var existingUser = await _authRepository.GetByEmailAsync(input.Email);
        if (existingUser != null)
            throw new Exception("Email already exists");
        
        var user = new User
        {
            Email = input.Email,
            PasswordHash = _passwordHelper.Hash(input.Password),
            Role = "User"
        };
        await _authRepository.AddAsync(user);
        await _authRepository.SaveChangesAsync();
    }

    public async Task<string> LoginAsync(LoginDto input)
    {
        var user = await _authRepository.GetByEmailAsync(input.Email);
        
        if (user == null)
            throw new Exception("User not found");

        if (!_passwordHelper.Verify(input.Password, user.PasswordHash) || user.Email != input.Email)
            throw new Exception("Invalid email or password");
        
        return _tokenHelper.GenerateToken(user);
    }
}