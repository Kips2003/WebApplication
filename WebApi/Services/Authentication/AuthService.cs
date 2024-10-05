using System.Net.Mail;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Services.Authentication;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _token;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IEmailRepository _emailRepository;

    public AuthService(IUserRepository userRepository, ITokenService token, IPasswordHasher passwordHasher, IEmailRepository emailRepository)
    {
        _userRepository = userRepository;
        _token = token;
        _passwordHasher = passwordHasher;
        _emailRepository = emailRepository;
    }
    
    public async Task<AuthResponseDto> Register(UserRegisterDto request)
    {
        if (await _userRepository.UserExists(request.Email))
        {
            return new AuthResponseDto { Success = false, Message = "User already exists" };
        }

        if (!await _userRepository.BadEmail(request.Email))
        {
            return new AuthResponseDto { Success = false, Message = "Provided Email does is not suitable for email" };
        }

        if (!await _userRepository.BadPassword(request.Password))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Password Should be at least 8 characters with at least one uppercase and the digits"
            };
        }

        if (!await _userRepository.BadPhoneNumber(request.PhoneNumber))
        {
            return new AuthResponseDto
            {
                Success = false,
                Message = "Provided Password is not Suitable for phone number"
            };
        }

        var confirmationCode = _emailRepository.GenerateConfirmationCode();
        var token = _emailRepository.GenerateEmailConfirmationToken();
        _emailRepository.SendEmailConfirmationAsync(request.Email,confirmationCode);
        
        var user = new User
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CreatedAt = DateTime.Now,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            IsEmailConfirmed = false,
            EmailConfirmationToken = token
        };

        await _userRepository.CreateUser(user);

        //await SendConfirmation(request.Email, token);
        
        return new AuthResponseDto { Success = true, Message = "User Registered Successfully" };
    }

    public async Task<AuthResponseDto> LogIn(UserLoginDto request)
    {
        var user = await _userRepository.GetUserByEmail(request.Email);
        if (user == null)
        {
            return new AuthResponseDto
            {
                Success = false, Message = "Empty Filed"
            };
        }

        if (!user.IsEmailConfirmed)
        {
            return new AuthResponseDto { Success = false, Message = "Email not confirmed" };
        }
        if (!_passwordHasher.VerifyPassword(user.PasswordHash, request.Password))
        {
            return new AuthResponseDto { Success = false, Message = "Invalid Email or Password" };
        }

        var token = _token.GenerateToken(user);
        return new AuthResponseDto { Success = true, Token = token };
    }

    public async Task<UserDto> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetUserByEmail(email);

        if (user is null)
            return null;

        return new UserDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            PasswordHash = user.PasswordHash,
            DateOfCreate = user.CreatedAt,
            DateOfBirth = user.BirthDate,
            IsEmailConfirmed = user.IsEmailConfirmed,
            EmailConfirmationToken = user.EmailConfirmationToken
        };

    }

    public async Task UpdateUseAsync(User user)
    {
        await _userRepository.UpdateUserAsync(user);
    }


    public async Task<IEnumerable<UserDto>> GetUsersAsycn()
    {
        var users = await _userRepository.GetUsers();
        return users.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            FirstName = u.FirstName,
            LastName = u.LastName,
            PhoneNumber = u.PhoneNumber,
            Email = u.Email,
            PasswordHash = u.PasswordHash,
            DateOfCreate = u.CreatedAt,
            DateOfBirth = u.BirthDate,
            IsEmailConfirmed = u.IsEmailConfirmed,
            EmailConfirmationToken = u.EmailConfirmationToken
        });    
    }

    public async Task<User> FindByEmailConfirmationTokenAsync(string token)
    {
        return await _userRepository.FindByEmailConfirmationTokenAsync(token);
    }
}