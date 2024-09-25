using System.ComponentModel.DataAnnotations;

namespace WebApi.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime BirthDate { get; set; }
    public string PhoneNumber { get; set; }
    public int[] Reviews { get; set; }
//    public bool IsEmailConfirmed { get; set; }
  //  public string EmailConfirmationToken { get; set; }
}