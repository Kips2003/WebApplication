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
    
    public async Task SendEmailConfirmationAsync(string email,string token)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("gd-store.ge",_configuration["Smtp:SenderEmail"]));
        message.To.Add(new MailboxAddress(null, email));
        message.Subject = "Email Confirmation";
        
        string htmlContent = 
            $@"
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset='utf-8'>
                <link rel=""preconnect"" href=""https://fonts.googleapis.com"">
                <link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
                <link href=""https://fonts.googleapis.com/css2?family=Inter:ital,opsz,wght@0,14..32,100..900;1,14..32,100..900&display=swap"" rel=""stylesheet"">
                <title>Email Confirmation</title>
                <style>
                    body {{
                        font-family: Inter;
                        background-color: #f4f4f4;
                        padding: 20px;
                    }}
                    .container {{
                        max-width: 600px;
                        margin: auto;
                        background: white;
                        padding: 20px;
                        border-radius: 8px;
                        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    }}
                    h1 {{
                        color: #333;
                    }}
                    a {{
                        display: inline-block;
                        margin-top: 10px;
                        padding: 10px 15px;
                        background-color: #007bff;
                        color: white;
                        text-decoration: none;
                        border-radius: 5px;
                    }}
                    a:hover {{
                        background-color: #0056b3;
                    }}
                </style>
            </head>
            <body>
                <div class='container'>
                    <h1>Email Confirmation</h1>
                    <p>Your confirmation link is:</p>
                    <a href='https://gd-store.ge/confirm.html?token{token}'>Confirm your email</a>
                </div>
            </body>
            </html>";
        
        message.Body = new TextPart("html")
        {
            Text = htmlContent
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