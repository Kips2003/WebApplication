using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Models;
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
    /*[HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        var pendingUser = await _authService.FindByEmailConfirmationTokenAsync(token);

        if (pendingUser == null)
        {
            return BadRequest("Invalid token.");
        }

        // Create the user account
        var user = new User
        {
            UserName = pendingUser.UserName,
            FirstName = pendingUser.FirstName,
            LastName = pendingUser.LastName,
            Email = pendingUser.Email,
            PhoneNumber = pendingUser.PhoneNumber,
            PasswordHash = pendingUser.PasswordHash,
           // IsEmailConfirmed = true,
        };

        await _authService.UpdateUseAsync(user);

        // Optionally, remove the pending user from storage

        return Ok("Email confirmed and user account created successfully.");
    }*/
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody]UserRegisterDto request)
    {
        var response = await _authService.Register(request);

        if (!response.Success)
        {
            return BadRequest(new {message = response.Message});
        }

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto request)
    {
        var response = await _authService.LogIn(request);

        if (!response.Success)
        {
            return BadRequest(response.Message);
        }

        return Ok(response);
    }
}