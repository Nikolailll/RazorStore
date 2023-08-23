using System;
//using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Hosting;


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

            emailMessage.From.Add(new MailboxAddress("Stasik", "john.doe@example.org"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = htmlMessage
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("app.debugmail.io", 25, false);
                await client.AuthenticateAsync("8777630b-2004-4d0b-a3d9-6649d501ae24", "8ae1737e-6626-435f-8975-dc5ff4fb7fb1");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            } 
        }
    }
}
