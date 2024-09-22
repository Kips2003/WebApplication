using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Services.Authentication;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _authService.GetUsersAsycn();

        return Ok(users);
    }

    [HttpGet("{email}")]
    public async Task<IActionResult> GetUsersByEmail(string email)
    {
        var user = await _authService.GetUserByEmailAsync(email);

        if (user is null)
            return NotFound();

        return Ok(user);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        var response = await _authService.Register(request);

        if (!response.Success)
        {
            return BadRequest(new {message = response.Message});
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto request)
    {
        var response = await _authService.LogIn(request);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }
}