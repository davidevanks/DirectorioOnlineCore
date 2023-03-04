using Mailjet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utiles
{
    public interface IMailJetSender
    {
        public  Task<MailjetResponse> SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
