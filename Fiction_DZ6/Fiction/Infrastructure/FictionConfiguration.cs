namespace Fiction_DZ6.Infrastructure
{
    public class FictionConfiguration
    {
        public Email Email { get; set; }

        public Sms Sms { get; set; }
    }

    public class Email
    {
        public string SenderEmailAddress { get; set; }

        public string SenderEmailPassword { get; set; }
    }

    public class Sms
    {
        public string TwilioAccountSid { get; set; }

        public string TwilioAccountAuthToken { get; set; }

        public string TwilioAccountPhoneNumber { get; set; }

        public string RecipientPhoneNumber { get; set; }
    }
}
