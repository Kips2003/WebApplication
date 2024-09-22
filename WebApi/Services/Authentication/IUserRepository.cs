using WebApi.Models;

namespace WebApi.Services.Authentication;

public interface IUserRepository
{
    Task<bool> UserExists(string email);
    Task CreateUser(User user);
    Task<User> GetUserByEmail(string email);
    Task<IEnumerable<User>> GetUsers();
    Task<bool> BadEmail(string email);
    Task<bool> BadPassword(string password);
    Task<bool> BadPhoneNumber(string phoneNumber);
    Task<bool> EmptyRequiredField(User user);
}