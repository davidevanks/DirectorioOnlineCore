using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using Models.Models;
using Models.Models.EmailService;
using Models.Models.Identity.AccountViewModels;
using System;
using System.IO;

namespace APISeguridadWEB.ExtraServices.EmailService
{
    public interface IEmailService
    {
        #region Public Methods

        ResponseViewModel SendAccountConfirmationEmail(string to, string subject, int typeEmail,
            RegisterViewModel model, string urlAccountConfirmation);

        #endregion Public Methods
    }

    public class EmailService : IEmailService
    {
        #region Private Fields

        private readonly AppSettings _appSettings;
        private IWebHostEnvironment _env;
        private ResponseViewModel response = new ResponseViewModel();

        #endregion Private Fields

        #region Public Constructors

        public EmailService(IOptions<AppSettings> appSettings, IWebHostEnvironment env)
        {
            _appSettings = appSettings.Value;
            _env = env;
        }

        #endregion Public Constructors

        #region Public Methods

        public ResponseViewModel SendAccountConfirmationEmail(string to, string subject, int typeEmail, RegisterViewModel model, string urlAccountConfirmation)
        {
            try
            {
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse( _appSettings.EmailFrom));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                var builder = new BodyBuilder();
                string messageBody = "";

                //Se realiza validación para verificar tipo de correo y plantilla a usar (typeEmail 1=AccountConfirmation)
                switch (typeEmail)
                {
                    case 1:
                        var pathToFile = _env.ContentRootPath
                                         + Path.DirectorySeparatorChar.ToString()
                                         + "EmailTemplates"
                                         + Path.DirectorySeparatorChar.ToString()
                                         + "AccountEmailConfirmationTemplate.html";

                        using (StreamReader sourceReader = System.IO.File.OpenText(pathToFile))
                        {
                            builder.HtmlBody = sourceReader.ReadToEnd();
                        }

                        messageBody = builder.HtmlBody.Replace("%UserName%", model.Email).Replace("%urlAccountConfirmation%", urlAccountConfirmation);

                        break;

                    default:
                        throw new Exception("Unexpected Case");
                }

                email.Body = new TextPart(TextFormat.Html) { Text = messageBody };
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

        #endregion Public Methods

        #region Private Methods

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }

        #endregion Private Methods
    }
}