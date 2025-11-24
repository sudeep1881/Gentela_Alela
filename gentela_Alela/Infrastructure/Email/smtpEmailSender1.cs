

using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace gentela_Alela.Infrastructure.Email
{
    public sealed class smtpEmailSender1 : IEmailSender1
    {
        private readonly smtpOptions1 _options;
                      

        public smtpEmailSender1(IOptions<smtpOptions1> options)
        {
            _options = options.Value;


        }

        public async Task SendAsync(string toEmail, string subject, string htmlBody, string? plainTextBody)
        {
            using var message = new MailMessage
            {
                From = new MailAddress(_options.FromEmail, _options.FromName),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true,
            };
            message.To.Add(toEmail);

            if (!string.IsNullOrWhiteSpace(plainTextBody))
            {
                message.AlternateViews.Add(
                   AlternateView.CreateAlternateViewFromString(plainTextBody, null, "tet/html"));

                message.AlternateViews.Add(
                    AlternateView.CreateAlternateViewFromString(htmlBody, null, "text/html"));
            }
            using var client = new SmtpClient(_options.Host,_options.Port)
            {
                Credentials=new NetworkCredential(_options.UserName,_options.Password),
                EnableSsl=_options.EnableSsl
            };
            await client.SendMailAsync(message);
        }
    }
}
