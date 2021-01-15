using System;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Surveys.Infrastructure.Settings;
using Surveys.Infrastructure.Services.Interfaces;

namespace Surveys.Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailServiceSettings _emailOptions;
        private readonly ApplicationSettings _appOptions;

        public EmailSender(IOptions<EmailServiceSettings> emailOptions, IOptions<ApplicationSettings> appOptions)
        {
            _emailOptions = emailOptions.Value;
            _appOptions = appOptions.Value;
        }

        public async Task SendInvitationEmailAsync(string emailAddress, DateTime? startDate, DateTime? expirationDate)
        {
            SendGridMessage message = new SendGridMessage()
            {
                From = new EmailAddress(_emailOptions.SenderEmail, _emailOptions.SenderName),
                Subject = "Ankieta do wypełnienia",
                PlainTextContent = GetMessageBody(startDate, expirationDate),
                HtmlContent = GetMessageBody(startDate, expirationDate)
            };

            await SendMessage(_emailOptions.ApiKey, message, emailAddress);
        }

        public async Task SendMessage(string apiKey, SendGridMessage message, 
            string email)
        {
            SendGridClient client = new SendGridClient(apiKey);     
            
            message.AddTo(new EmailAddress(email));
            message.SetClickTracking(false, false);

            await client.SendEmailAsync(message);
        }

        private string GetMessageBody(DateTime? startDate, DateTime? expirationDate)
        {
            string body = "Zostałeś zaproszony do wypełnienia ankiety. ";

            body += $"Ankietę możesz wypełnić od {GetDate(startDate)} ";

            if (expirationDate != null)
                body += $"Możesz ją wypełnić, do {GetDate(expirationDate)}. " +
                    $"Po tym czasie ankieta nie będzie aktywna. ";

            body += $"Wejdź na {_appOptions.ClientUrl} i wypełnij ankietę.";

            return body;
        }

        private string GetDate(DateTime? date)
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
