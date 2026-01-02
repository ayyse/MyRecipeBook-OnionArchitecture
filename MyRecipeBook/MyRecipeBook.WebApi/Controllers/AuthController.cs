using Microsoft.AspNetCore.Mvc;
using MyRecipeBook.Application.Dtos.User;
using MyRecipeBook.Application.Interfaces.AppServices;

namespace MyRecipeBook.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthAppService _authAppService;

    public AuthController(IAuthAppService authAppService)
    {
        _authAppService = authAppService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto input)
    {
        await _authAppService.RegisterAsync(input);
        return Ok("User registered");
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthenticatedDto>> Login(LoginDto input)
    {
        var token = await _authAppService.LoginAsync(input);
        return Ok(new AuthenticatedDto()
        {
            Token = token
        });
    }
}