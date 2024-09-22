using WebApi.DTO;

namespace WebApi.Services.Authentication;

public interface IAuthService
{
    Task<AuthResponseDto> Register(UserRegisterDto request);
    Task<AuthResponseDto> LogIn(UserLoginDto request);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task<IEnumerable<UserDto>> GetUsersAsycn();
    Task SendConfirmation(string email, string token);
}