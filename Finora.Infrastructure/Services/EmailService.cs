using Finora.Application.Common.Models;
using Finora.Application.Interfaces.Services;
using Microsoft.Extensions.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
namespace Finora.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(
            string to,
            string subject,
            string body,
            byte[]? attachment = null,
            string? attachmentName = null)
        {
            var message = new MimeMessage();

            message.From.Add(
                new MailboxAddress(
                    _emailSettings.FromName,
                    _emailSettings.FromEmail));

            message.To.Add(MailboxAddress.Parse(to));

            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = body
            };

            if (attachment != null &&
                !string.IsNullOrWhiteSpace(attachmentName))
            {
                builder.Attachments.Add(
                    attachmentName,
                    attachment);
            }

            message.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                _emailSettings.Host,
                _emailSettings.Port,
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                _emailSettings.Username,
                _emailSettings.Password);

            await smtp.SendAsync(message);

            await smtp.DisconnectAsync(true);
        }
    }
}
