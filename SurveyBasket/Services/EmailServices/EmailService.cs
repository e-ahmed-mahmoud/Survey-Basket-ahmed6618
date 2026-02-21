using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using SurveyBasket.Extensions.Emails;

namespace SurveyBasket.Services;

public class EmailService(IOptions<EmailSettings> options) : IEmailSender
{
    private readonly EmailSettings _emailSettings = options.Value;

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var message = new MimeMessage()
        {
            Sender = MailboxAddress.Parse(_emailSettings.Email),
            Subject = subject
        };
        message.To.Add(MailboxAddress.Parse(email));    //use MailBoxAddress to parse string as email

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
        message.Body = bodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();
        // Bypass SSL certificate validation (DEVELOPMENT ONLY!)
        smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

        smtpClient.Connect(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.StartTls);
        smtpClient.Authenticate(_emailSettings.Email, _emailSettings.Password);
        await smtpClient.SendAsync(message);
        smtpClient.Disconnect(quit: true);
    }
}
