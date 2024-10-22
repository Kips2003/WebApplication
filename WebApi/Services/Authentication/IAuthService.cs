using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Services.Authentication;

public interface IAuthService
{
    Task<AuthResponseDto> Register(UserRegisterDto request);
    Task<AuthResponseDto> LogIn(UserLoginDto request);
    Task<UserDto> GetUserByEmailAsync(string email);
    Task UpdateUseAsync(User user);
    Task<IEnumerable<UserDto>> GetUsersAsycn();

    Task<UserDto> GetUserById(int id);
  //  Task SendConfirmation(string email, string token);
   public Task<User> FindByEmailConfirmationTokenAsync(string token);
}