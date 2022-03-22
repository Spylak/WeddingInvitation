using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace InvitationAPI;


public class EmailSender : IEmailSender
{
    private readonly MailJetSettings _mailJetSettings;
    public EmailSender(IOptions<MailJetSettings> mailjetSettings)
    {
        _mailJetSettings = mailjetSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        MailMessage mail = new MailMessage();
        SmtpClient SmtpServer = new SmtpClient("in-v3.mailjet.com");
        mail.From = new MailAddress("laksmith@protonmail.com");
        mail.To.Add(email);
        mail.Subject = subject;
        mail.Body = $"{htmlMessage}";
        mail.IsBodyHtml = true;
        SmtpServer.Port = 587;
        SmtpServer.Credentials = new System.Net.NetworkCredential(_mailJetSettings.PublicKey, _mailJetSettings.PrivateKey);
        SmtpServer.EnableSsl = true;
            
        SmtpServer.Send(mail);
    }


}