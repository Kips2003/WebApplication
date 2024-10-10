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

    public async Task<User> getUserById(int id)
    {
        var user = await _context.Users
            .Include(u => u.Addresses)
            .Include(u => u.Products)
            .Include(u => u.CartItems)
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null;

        return new User
        {
            Id = user.Id,
            PrivilageId = user.PrivilageId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UserName = user.UserName,
            Email = user.Email,
            PasswordHash = user.PasswordHash,
            CreatedAt = user.CreatedAt,
            BirthDate = user.BirthDate,
            PhoneNumber = user.PhoneNumber,
            IsEmailConfirmed = user.IsEmailConfirmed,
            EmailConfirmationToken = user.EmailConfirmationToken,
            ProfilePicture = user.ProfilePicture,
            Products = user.Products.ToList(),
            Addresses = user.Addresses.ToList(),
            CartItems = user.CartItems.ToList(),
            Reviews = user.Reviews.ToList(),
            Orders = user.Orders.ToList()
        };    
    }

    public async Task<User> GetUserByEmail(string email)
    {
        var user = await _context.Users
            .Include(u => u.Addresses)
            .Include(u => u.Products)
            .Include(u => u.CartItems)
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return null;

        return user;

        /*return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);*/
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        var users = await _context.Users
            .Include(u => u.Addresses)
            .Include(u => u.Products)
            .Include(u => u.CartItems)
            .Include(u => u.Reviews)
            .Include(u => u.Orders)
            .ToListAsync();

        return users;
        /*return await _context.Users.ToListAsync();*/
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

    public async Task<bool> CheckForProfilePictureAsync(string picturePath)
    {
        string regex = @"^/images/";

        if (Regex.IsMatch(picturePath, regex))
            return true;

        return false;
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