using System;
//using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;


namespace RazorStore.Services
{
	public class EmailSender : IEmailSender
	{
		public EmailSender()
		{
		}

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailMessage = new MimeMessage();

            //emailMessage.From.Add(new MailboxAddress("Stasik", "Stasik@stason.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("connect.smtp.bz", 465, true);
                await client.AuthenticateAsync("ivansteroidov@yandex.ru", "w9C9kr2S8o9W");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            } 
        }
    }
}

