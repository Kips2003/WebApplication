using WebApi.Models;

namespace WebApi.Services.Authentication;

public interface ITokenService
{
    string GenerateToken(User user);
}