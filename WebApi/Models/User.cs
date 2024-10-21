using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public int PrivilageId  { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsEmailConfirmed { get; set; }
    public string EmailConfirmationToken { get; set; }
    public string ProfilePicture { get; set; }
    public ICollection<Reviews> Reviews { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public ICollection<Cart> CartItems { get; set; }
    public ICollection<Order> Orders { get; set; }
    public ICollection<Product> Products { get; set; }
}