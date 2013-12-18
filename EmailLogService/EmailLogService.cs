using System.Net.Mail;
using Ninject_FakeItEasyDemo.Infrastructure;

namespace EmailLoging
{
    public class EmailLogService : ILogService
    {
        public void Log(string message)
        {
            var mail = new MailMessage("you@yourcompany.com", "user@hotmail.com");
            var client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.google.com"
            };
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
        }
    }
}
