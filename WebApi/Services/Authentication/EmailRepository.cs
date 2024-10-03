using System.Security.Cryptography;
using MimeKit;

namespace WebApi.Services.Authentication;
using MailKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

public class EmailRepository : IEmailRepository
{
    private readonly IConfiguration _configuration;

    public EmailRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public async Task SendEmailConfirmationAsync(string email,int confirmationCode)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("gd-store.ge",_configuration["Smtp:SenderEmail"]));
        message.To.Add(new MailboxAddress(null, email));
        message.Subject = "Email Confirmation";
        message.Body = new TextPart("plain")
        {
            Text =
                $"Your confirmation code is: <b>{confirmationCode}</b>"
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]),
            MailKit.Security.SecureSocketOptions.StartTls);
        await client.AuthenticateAsync(_configuration["Smtp:UserName"], _configuration["Smtp:Password"]);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
    public string GenerateEmailConfirmationToken()
    {
        return Guid.NewGuid().ToString();
    }
    
    public int GenerateConfirmationCode()
    {
        // Generate a 6-digit random number for confirmation
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] randomNumber = new byte[4];
            rng.GetBytes(randomNumber);
            int result = BitConverter.ToInt32(randomNumber, 0);
            return Math.Abs(result % 1000000);  // Ensure it's a 6-digit number
        }
    }
}