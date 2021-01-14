using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using Surveys.Infrastructure.Services.Interfaces;
using Surveys.Infrastructure.Settings;
using System;
using System.Threading.Tasks;

namespace Surveys.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailServiceOptions _emailOptions;
        private readonly ApplicationSettings _appOptions;

        public EmailSender(IOptions<EmailServiceOptions> emailOptions, IOptions<ApplicationSettings> appOptions)
        {
            _emailOptions = emailOptions.Value;
            _appOptions = appOptions.Value;
        }

        public Task SendInvitationEmailAsync(string emailAddress, DateTime? startDate, DateTime? expirationDate)
        {
            //var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var message = new SendGridMessage()
            {
                From = new EmailAddress(_emailOptions.SenderEmail, _emailOptions.SenderName),
                Subject = "Ankieta do wypełnienia",
                PlainTextContent = GetMessageBody(startDate, expirationDate),
                HtmlContent = GetMessageBody(startDate, expirationDate)
            };

            return Execute(_emailOptions.ApiKey, message, emailAddress);
        }
        public Task Execute(string apiKey, SendGridMessage message, string email)
        {
            var client = new SendGridClient(apiKey);            
            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);

            return client.SendEmailAsync(message);
        }

        private string GetMessageBody(DateTime? startDate, DateTime? expirationDate)
        {
            var body = "Zostałeś zaproszony do wypełnienia ankiety. ";

            body += $"Ankietę możesz wypełnić od {getDate(startDate)} ";

            if (expirationDate != null)
                body += $"Możesz ją wypełnić, do {getDate(expirationDate)}. " +
                    $"Po tym czasie ankieta nie będzie aktywna. ";

            body += $"Wejdź na {_appOptions.ClientUrl} i wypełnij ankietę.";

            return body;
        }

        private string getDate(DateTime? date)
        {
            var day = date.Value.Day.ToString().PadLeft(2, '0');
            var month = date.Value.Month.ToString().PadLeft(2, '0');
            var year = date.Value.Year.ToString();
            var hour = date.Value.Hour.ToString().PadLeft(2, '0');
            var minute = date.Value.Minute.ToString().PadLeft(2, '0');

            return day + "/" + month + "/" + year + " " + hour + ":" + minute;
        }

    }
}
