namespace Fiction_DZ6.Infrastructure
{
    public interface IFictionConfiguration
    {
        IEmail Email { get; set; }

        ISms Sms { get; set; }

        IExternalImageService ExternalImageService { get; set; }

        string ImageName { get; set; }
    }

    public interface IEmail
    {
        string SenderEmailAddress { get; set; }

        string SenderEmailPassword { get; set; }
    }

    public interface ISms
    {
        string TwilioAccountSid { get; set; }

        string TwilioAccountAuthToken { get; set; }

        string TwilioAccountPhoneNumber { get; set; }

        string RecipientPhoneNumber { get; set; }
    }

    public interface IExternalImageService
    {
        string ExternalImageServiceUrl { get; set; }

        string ExternalImageServiceResource { get; set; }

        string ExternalImageServiceQueryParameter { get; set; }
    }
}
