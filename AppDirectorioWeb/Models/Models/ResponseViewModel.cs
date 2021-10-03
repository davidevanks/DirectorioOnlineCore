using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models.Models
{
   public class ResponseViewModel
    {
       // IdentityResult result
       public IdentityResult IdentityResult { get; set; }
       public SignInResult SignInResult { get; set; }
       public string MessageResponse { get; set; }
       public MessageCode MessageResponseCode { get; set; }
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
