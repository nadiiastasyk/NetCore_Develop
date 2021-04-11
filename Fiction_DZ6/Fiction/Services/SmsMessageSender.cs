using Fiction_DZ6.Infrastructure;
using Microsoft.Extensions.Options;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Fiction_DZ6.Services
{
    public class SmsMessageSender : IMessageSender
    {
        private readonly FictionConfiguration _configuration;
        public SmsMessageSender(IOptions<FictionConfiguration> options)
        {
            _configuration = options.Value;
        }
        public void SendMessage()
        {
            TwilioClient.Init(_configuration.TwilioAccountSid, _configuration.TwilioAccountAuthToken);

            var message = MessageResource.Create(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber(_configuration.TwilioAccountPhoneNumber),
                to: new Twilio.Types.PhoneNumber(_configuration.RecipientPhoneNumber)
            );

            Console.WriteLine(message.Sid);
        }
    }
}
