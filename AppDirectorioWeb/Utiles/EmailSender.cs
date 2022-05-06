using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;
        public EmailSender(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute( email,  subject,  htmlMessage);
        }

        public async Task Execute(string email, string subject, string htmlMessage)
        {
            //string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
            //StreamReader str = new StreamReader(FilePath);
            //string MailText = str.ReadToEnd();
            //str.Close();
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
        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }

    }
}
