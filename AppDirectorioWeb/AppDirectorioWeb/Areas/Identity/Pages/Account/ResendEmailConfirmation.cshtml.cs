using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Utiles;

namespace AppDirectorioWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMailJetSender _mailJetSender;

        public ResendEmailConfirmationModel(UserManager<IdentityUser> userManager, IMailJetSender mailJetSender)
        {
            _userManager = userManager;
            _mailJetSender = mailJetSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage ="Email es requerido!")]
            [EmailAddress(ErrorMessage ="Email no válido!")]
            public string Email { get; set; }
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Correo inválido.");
                return Page();
            }

            var userId = await _userManager.GetUserIdAsync(user);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = userId, code = code },
                protocol: Request.Scheme);

            string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateConfirmEmail.html";
            StreamReader str = new StreamReader(FilePath);
            string MailText = str.ReadToEnd();
            str.Close();

            MailText = MailText.Replace("[username]", Input.Email).Replace("[linkRef]", HtmlEncoder.Default.Encode(callbackUrl));


          var jetResult=  await _mailJetSender.SendEmailAsync(Input.Email, "Verificación de Cuenta", MailText);
           
            if (jetResult.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "Correo de verificación enviado. Favor revisa tu email.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Error enviando correo, contacta a soporte técnico o intenta más tarde.");
            }
          
            return Page();
        }
    }
}
