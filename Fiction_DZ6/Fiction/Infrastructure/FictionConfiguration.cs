namespace Fiction_DZ6.Infrastructure
{
    public class FictionConfiguration : IFictionConfiguration
    {
        public IEmail Email { get; set; }

        public ISms Sms { get; set; }

        public IExternalImageService ExternalImageService { get; set; }

        public string ImageName { get; set; }
    }

    public class Email : IEmail
    {
        public string SenderEmailAddress { get; set; }

        public string SenderEmailPassword { get; set; }
    }

    public class Sms : ISms
    {
        public string TwilioAccountSid { get; set; }

        public string TwilioAccountAuthToken { get; set; }

        public string TwilioAccountPhoneNumber { get; set; }

        public string RecipientPhoneNumber { get; set; }
    }

    public class ExternalImageService : IExternalImageService
    {
        public string ExternalImageServiceUrl { get; set; }

        public string ExternalImageServiceResource { get; set; }

        public string ExternalImageServiceQueryParameter { get; set; }
    }
}
