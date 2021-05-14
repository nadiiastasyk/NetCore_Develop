using CocktailBar.Infrastructure;
using CocktailBar.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CocktailBar.Services
{
    public class EmailMessageSender : IMessageSender
    {
        private readonly CocktailBarConfiguration _configuration;

        public EmailMessageSender(IOptions<CocktailBarConfiguration> options)
        {
            _configuration = options.Value;
        }

        public void SendConfirmationMessage(User user, string emailConfirmationUrl)
        {
            //Declare email message
            var message = new MimeMessage();

            var from = new MailboxAddress("Administrator", _configuration.SenderEmailAddress);
            message.From.Add(from);

            var to = new MailboxAddress(user.FirstName, user.Email);
            message.To.Add(to);

            message.Subject = "Cocktail Bar registration confirmation";

            //Add email body
            BodyBuilder bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $"Please confirm your account by clicking this link: <a href='{emailConfirmationUrl}'>link</a>";

            message.Body = bodyBuilder.ToMessageBody();

              //Send message
            using (SmtpClient client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (sender, certificate, certChainType, errors) => true;

                // smtp-mail.outlook.com   587   MailKit.Security.SecureSocketOptions.StartTls
                client.Connect("smtp.gmail.com", 465, true);
                client.Authenticate(_configuration.SenderEmailAddress, _configuration.SenderEmailPassword);

                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
