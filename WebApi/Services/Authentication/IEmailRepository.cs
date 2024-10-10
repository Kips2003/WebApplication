namespace WebApi.Services.Authentication;

public interface IEmailRepository
{
    public string GenerateEmailConfirmationToken();
    public Task SendEmailConfirmationAsync(string email,string token);
    public Task GivePrivilegeToUser(string email, string token, string privilege);
}