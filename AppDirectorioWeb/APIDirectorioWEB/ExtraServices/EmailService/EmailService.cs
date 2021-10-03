using System;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Models.Models;
using Models.Models.EmailService;


namespace APISeguridadWEB.ExtraServices.EmailService
{
    public interface IEmailService
    {
        ResponseViewModel Send(string to, string subject, string html, string from = null);
    }

    public class EmailService : IEmailService
    {
        private readonly AppSettings _appSettings;
        private ResponseViewModel response = new ResponseViewModel();

        public EmailService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public ResponseViewModel Send(string to, string subject, string html, string from = null)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(from ?? _appSettings.EmailFrom));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                System.Net.ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
                using var smtp = new SmtpClient();
                smtp.Connect(_appSettings.SmtpHost, _appSettings.SmtpPort, false);
                smtp.Authenticate(_appSettings.SmtpUser, _appSettings.SmtpPass);
                smtp.Send(email);
                smtp.Disconnect(true);
                response.MessageResponse = "Correo enviado exitosamente";
                response.MessageResponseCode = ResponseViewModel.MessageCode.Success;

            }
            catch (Exception e)
            {
                response.MessageResponse = "Error al enviar correo";
                response.MessageResponseCode = ResponseViewModel.MessageCode.Failed;
                throw;
            }

            return response;
        }

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }
    }
}