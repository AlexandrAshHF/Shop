using CursVN.Core.Abstractions.Other;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace CursVN.Application.Other
{
    public class EmailService : IEmailService
    {
        private string _sender;
        private string _password;
        public EmailService(string sender, string password)
        {
            _sender = sender;
            _password = password;
        }
        public async Task SendMail(string message, string consumerEmail)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Shop", _sender));
            emailMessage.To.Add(new MailboxAddress("Client", consumerEmail));
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using(var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_sender, _password);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
    }
}
