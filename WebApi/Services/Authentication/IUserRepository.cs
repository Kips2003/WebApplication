using WebApi.Models;

namespace WebApi.Services.Authentication;

public interface IUserRepository
{
    Task<bool> UserExists(string email);
    Task CreateUser(User user);
    Task<User> GetUserByEmail(string email);
    public Task UpdateUserAsync(User user);
    Task<IEnumerable<User>> GetUsers();
    Task<bool> BadEmail(string email);
    Task<bool> BadPassword(string password);
    Task<bool> BadPhoneNumber(string phoneNumber);
    /*public Task<User> FindByEmailConfirmationTokenAsync(string token);*/
    Task<bool> EmptyRequiredField(User user);
    public Task<User> FindByEmailConfirmationTokenAsync(string token);
}