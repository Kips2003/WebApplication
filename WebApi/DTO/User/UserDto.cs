namespace WebApi.DTO;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime DateOfCreate { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}