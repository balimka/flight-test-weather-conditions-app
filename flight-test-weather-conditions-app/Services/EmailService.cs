using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using flight_test_weather_conditions_app.Services.Interfaces;

namespace flight_test_weather_conditions_app.Services
{
    public class EmailService : IEmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly bool enableSsl;

        public EmailService(IConfiguration configuration)
        {
            IConfigurationSection emailConfig = configuration.GetSection("email");
            smtpServer = emailConfig["smtpServer"];
            smtpPort = int.Parse(emailConfig["smtpPort"]);
            enableSsl = bool.Parse(emailConfig["enableSsl"]);
        }

        public void SendEmailWithAttachment(string senderEmail, string password, string receiverEmail, string subject, string body, string attachmentFileName)
        {
            try
            {
                using MailMessage mail = new(senderEmail, receiverEmail);
                mail.Subject = subject;
                mail.Body = body;

                Attachment attachment = new(attachmentFileName);
                mail.Attachments.Add(attachment);

                using SmtpClient smtpClient = new(smtpServer, smtpPort);
                smtpClient.EnableSsl = enableSsl;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, password);

                smtpClient.Send(mail);

                mail.Dispose();
                smtpClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while sending the email: " + ex.Message);
            }
        }
    }
}
