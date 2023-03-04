using Mailjet.Client;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public class MailJetSender: IMailJetSender
    {

        private readonly MailJetSettings _mailJetSettings;

        public MailJetSender(IOptions<MailJetSettings> mailJetSettings)
        {
            _mailJetSettings = mailJetSettings.Value;
        }
        public async Task<MailjetResponse> SendEmailAsync(string email, string subject, string htmlMessage)
        {
            try
            {
                MailjetClient client = new MailjetClient(_mailJetSettings.ApiKey, _mailJetSettings.SecretKey);


                MailjetRequest request = new MailjetRequest
                {
                    Resource = SendV31.Resource,
                }.Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", "info@brujulapyme.com"},
                  {"Name", "Info Brújula Pyme"}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", email},
                   {"Name", ""}
                   }
                  }},
                 {"Subject", subject},
                 {"TextPart", ""},
                 {"HTMLPart", htmlMessage}
                 }
                       });
              
                MailjetResponse response = await client.PostAsync(request);
                return response;


            }
            catch (System.Exception ex)
            {

                throw;
            }
        }
    }
}
