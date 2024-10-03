namespace WebApi.Services.Authentication;

public interface IEmailRepository
{
    public string GenerateEmailConfirmationToken();
    public Task SendEmailConfirmationAsync(string email,int confirmationCode);
    public int GenerateConfirmationCode();
}