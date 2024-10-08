using WebApi.Models;

namespace WebApi.DTO;

public class UserDto
{
    public int Id { get; set; }
    public int PrevelageId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfCreate { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string EmailConfirmationToken { get; set; }
    public string ProfilePicture { get; set; }

    public ICollection<Models.Reviews> ReviewsCollection { get; set; }
    public ICollection<Models.Address> Addresses { get; set; }
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<Models.Order> Orders { get; set; }
}