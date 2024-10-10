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
    [HttpGet("confirm-email/{token}")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        // Find the user by the confirmation token
        var pendingUser = await _authService.FindByEmailConfirmationTokenAsync(token);

        if (pendingUser == null)
        {
            return BadRequest("Invalid token.");
        }

        // Update the existing user's properties
        pendingUser.IsEmailConfirmed = true;

        // Call the update method
        await _authService.UpdateUseAsync(pendingUser);

        return Ok("Email confirmed and user account updated successfully.");
    }

    [HttpGet("givePrivilege/{privilegeId}/{token}")]
    public async Task<IActionResult> GivePrivilege(int privilegeId, string token)
    {
        var user = await _authService.FindByEmailConfirmationTokenAsync(token);

        user.PrivilageId = privilegeId;

        await _authService.UpdateUseAsync(user);

        return Ok(User);
    }
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