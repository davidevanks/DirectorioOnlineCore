using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Models.Identity.AccountViewModels
{
    public class ResponseViewModel
    {
        public string MessageResponse { get; set; }
        public MessageCode MessageResponseCode { get; set; }
        public TokenViewModel Token { get; set; }
        public enum MessageCode
        {
            Success,
            EmailNotConfirmed,
            IncorrectPassword,
            UserNotExist,
            Failed,
            InvalidInformation,
            EmailConfirmedSuccess

        }
    }
}
