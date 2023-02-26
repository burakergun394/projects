using MimeKit;
using System.Globalization;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace PusulaGroup.WebApp.Core.Helpers
{
    public class EmailHelper
    {
        private readonly ILogger<EmailHelper> logger;

        public EmailHelper(ILogger<EmailHelper> logger)
        {
            this.logger = logger;
        }

        public async Task SendEmailAsync(SendEmailParams @params)
        {
            try
            {
                var mail = new MailMessage();
                var smtpClient = new SmtpClient(@params.Host);
                mail.From = new MailAddress(@params.UserName);
                mail.To.Add(@params.To);
                mail.Subject = @params.Subject;
                mail.Body = @params.Body;
                smtpClient.Port = @params.Port;
                smtpClient.EnableSsl = false;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(@params.UserName, @params.Password);
                await smtpClient.SendMailAsync(mail);
            }
           catch(Exception ex)
            {
                logger.LogError(ex, "An error occured when sending mail.");
            }
        }
    }

    public class SendEmailParams
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public int Port { get; set; }
        public string To { get; set; }
    }
}
