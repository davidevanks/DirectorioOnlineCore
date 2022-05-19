using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Threading.Tasks;

namespace Utiles
{
    public class EmailSender : IEmailSender
    {
        #region Private Fields

        private readonly MailSettings _mailSettings;

        #endregion Private Fields

        #region Public Constructors

        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        #endregion Public Constructors

        #region Public Methods

        public async Task Execute(string email, string subject, string htmlMessage)
        {
            string MailText = htmlMessage;
            MailText = MailText.Replace("[username]", email).Replace("[email]", email);
            var emailMime = new MimeMessage();
            emailMime.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            emailMime.To.Add(MailboxAddress.Parse(email));
            emailMime.Subject = subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = MailText;
            emailMime.Body = builder.ToMessageBody();

            System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            await smtp.SendAsync(emailMime);
            smtp.Disconnect(true);
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(email, subject, htmlMessage);
        }

        #endregion Public Methods

        #region Private Methods

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #endregion Private Methods
    }
}