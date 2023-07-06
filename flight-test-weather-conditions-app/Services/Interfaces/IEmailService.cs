namespace flight_test_weather_conditions_app.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmailWithAttachment(string senderEmail, string password, string receiverEmail, string subject, string body, string attachmentFileName);
    }
}
