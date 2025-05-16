using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using Store.Services.AuthAPI.Options;

namespace Store.Services.AuthAPI.Services;

public class EmailService(IOptions<MailOptions> mailSetting) : IEmailSender
{
    private readonly MailOptions _mailSettings = mailSetting.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage
        {
            Sender = MailboxAddress.Parse(_mailSettings.Mail),
            Subject = subject
        };

        message.To.Add(MailboxAddress.Parse(email));

        var builder = new BodyBuilder
        {
            HtmlBody = htmlMessage
        };

        message.Body = builder.ToMessageBody();

        using var smtp = new SmtpClient();
        smtp.Connect(_mailSettings.Host, port: _mailSettings.Port, options: SecureSocketOptions.StartTls);
        smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        await smtp.SendAsync(message);
        smtp.Disconnect(true);
    }
}
