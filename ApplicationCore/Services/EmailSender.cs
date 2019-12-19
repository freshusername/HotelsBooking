﻿using ApplicationCore.Infrastructure;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
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
        private readonly EmailOptions _emailOptions;
       
        public EmailSender(IOptions<EmailOptions> emailOptions)
        {
            _emailOptions = emailOptions.Value;
        }
   
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            try
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

                    await client.ConnectAsync(_emailOptions.Host, _emailOptions.Port, _emailOptions.EnableSsl);
                    await client.AuthenticateAsync(_emailOptions.Account, _emailOptions.Password);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(ex.Message);
            }
        }
    }
}

