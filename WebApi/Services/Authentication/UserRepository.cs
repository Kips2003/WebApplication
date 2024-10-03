using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Utils;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Services.Authentication;

public class UserRepository : IUserRepository
{
    private readonly DataContext _context;

    public UserRepository(DataContext context)
    {
        _context = context;
    }
    
    public async Task<bool> UserExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<bool> BadEmail(string email)
    {
        string regex = @"^\S+@\S+$";

        if (Regex.IsMatch(email,regex))
            return true;

        return false;
    }

    public async Task<bool> BadPassword(string password)
    {
        string regex = @"^(?=.*[A-Z])(?=(.*\d){3,})[A-Za-z\d]{8,}$";

        if (Regex.IsMatch(password,regex))
            return true;

        return false;
    }

    public async Task<bool> BadPhoneNumber(string phoneNumber)
    {
        string regex = @"^\d{1,3}-?\d{1,3}-?\d{1,3}$";

        if (Regex.IsMatch(phoneNumber, regex))
            return true;

        return false;
    }

    public async Task<User> FindByEmailConfirmationTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
    public async Task<bool> EmptyRequiredField(User user)
    {
        /*if (user.UserName.IsNullOrEmpty() && user.FirstName.IsNullOrEmpty() && user.LastName.IsNullOrEmpty() &&
            user.Email.IsNullOrEmpty() && user.PhoneNumber.IsNullOrEmpty())
            return false;*/

        return true;
    }
}