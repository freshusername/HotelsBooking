using ApplicationCore.Infrastructure;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IOptions<EmailOptions> _senderOptions;
        public EmailSender(IOptions<EmailOptions> options)
        {
            _senderOptions = options;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Support", "admin@admin.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_senderOptions.Value.Host, _senderOptions.Value.Port, _senderOptions.Value.EnableSsl);
                await client.AuthenticateAsync(_senderOptions.Value.Account, _senderOptions.Value.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}

