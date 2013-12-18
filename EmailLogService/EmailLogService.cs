using System.Net.Mail;
using Ninject_FakeItEasyDemo.Infrastructure;

namespace EmailLoging
{
    public class EmailLogService : ILogService
    {
        private readonly string _host;
        private readonly string _recipient;

        public EmailLogService(string host, string recipient)
        {
            _host = host;
            _recipient = recipient;
        }

        public void Log(string message)
        {
            var mail = new MailMessage(_recipient, _recipient);
            var client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = _host
            };
            mail.Subject = "this is a test email.";
            mail.Body = "this is my test email body";
            client.Send(mail);
        }
    }
}
