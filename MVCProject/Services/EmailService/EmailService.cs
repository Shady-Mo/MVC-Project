using Microsoft.Extensions.Options;
using MVCProject.Helpers;
using System.Net;
using System.Net.Mail;

namespace MVCProject.Services.EmailService {
    public class EmailService : IEmailService {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> options) {
            _emailSettings = options.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body) {
            var message = new MailMessage(_emailSettings.SenderEmail, toEmail, subject, body);
            message.IsBodyHtml = true;

            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port) {
                Credentials = new NetworkCredential(_emailSettings.SenderUserName, _emailSettings.SenderPassword),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}
