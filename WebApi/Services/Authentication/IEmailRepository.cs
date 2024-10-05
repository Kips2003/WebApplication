namespace WebApi.Services.Authentication;

public interface IEmailRepository
{
    public string GenerateEmailConfirmationToken();
    public Task SendEmailConfirmationAsync(string email,string token);
    public int GenerateConfirmationCode();
}