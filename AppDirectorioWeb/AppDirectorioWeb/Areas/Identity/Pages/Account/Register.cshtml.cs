using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Utiles;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace AppDirectorioWeb.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;


        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required(ErrorMessage = "El nombre completo es requerido")]
            [Display(Name = "Nombre Completo")]
            public string FullName { get; set; }

            [Required(ErrorMessage ="El email es requerido")]
            [EmailAddress(ErrorMessage ="Email no valido")]
            [Display(Name = "Email")]
            
            public string Email { get; set; }

            [Required(ErrorMessage = "El número de telefono es requerido")]
            [Phone(ErrorMessage ="télefono no valido")]
            [Display(Name = "Número de telefono")]
            public string Telefono { get; set; }

            [Required(ErrorMessage = "El password es requerido")]
            [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Required(ErrorMessage = "La confirmación de password es requerido")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar password")]
            [Compare("Password", ErrorMessage = "El password y la confirmación de password no coinciden.")]
            public string ConfirmPassword { get; set; }
            [Display(Name = "Que tipo de usuario eres?")]
            [Required(ErrorMessage = "El tipo de cliente es requerido")]
            public string Role { get; set; }
           
            public IEnumerable<SelectListItem> RoleList { get; set; }
            public bool NotificationsPromo { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (User.IsInRole(SP.Role_Admin))
            {
                Input = new InputModel()
                {
                    RoleList = _roleManager.Roles.Select(x => x.Name).Select(i => new SelectListItem
                    {
                        Text = i,
                        Value = i
                    })
                };
            }
            else
            {
                Input = new InputModel()
                {
                    RoleList = _roleManager.Roles.Where(x => x.Name == SP.Role_Customer).Select(x => x.Name).Select(i => new SelectListItem
                    {
                        Text = i,
                        Value = i
                    })
                };

                Input.Role = SP.Role_Customer;
            }
          

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email,PhoneNumber=Input.Telefono };
              

                var result = await _userManager.CreateAsync(user, Input.Password);
               
                string idUserCreate = "";
                if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
                {
                    idUserCreate = user.Id;
                }else
                {
                    idUserCreate = HttpContext.Session.GetString("UserId");
                }
               

                var userDetail = new UserDetail { UserId = user.Id, FullName = Input.FullName,NotificationsPromo=Input.NotificationsPromo, RegistrationDate = DateTime.Now,IdUserCreate= idUserCreate };
                  _unitOfWork.UserDetail.Add(userDetail);
                if (result.Succeeded)
                {
                    //_logger.LogInformation("User created a new account with password.");

                   

                    if (! await _roleManager.RoleExistsAsync(SP.Role_Admin))
                    {
                       await _roleManager.CreateAsync(new IdentityRole(SP.Role_Admin));
                    }
                    if (!await _roleManager.RoleExistsAsync(SP.Role_BusinesAdmin))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SP.Role_BusinesAdmin));
                    }
                    if (!await _roleManager.RoleExistsAsync(SP.Role_Customer))
                    {
                        await _roleManager.CreateAsync(new IdentityRole(SP.Role_Customer));
                    }

                   
                        await _userManager.AddToRoleAsync(user, Input.Role);


                


                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    string FilePath = Directory.GetCurrentDirectory() + "\\wwwroot\\EmailTemplates\\TemplateConfirmEmail.html";
                    StreamReader str = new StreamReader(FilePath);
                    string MailText = str.ReadToEnd();
                    str.Close();

                    MailText = MailText.Replace("[username]", Input.Email).Replace("[linkRef]", HtmlEncoder.Default.Encode(callbackUrl));

                    await _emailSender.SendEmailAsync(Input.Email, "Verificación de Cuenta", MailText);

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        if (User.IsInRole(SP.Role_Admin))
                        {
                            //admin is registering  new user
                            return RedirectToAction("Index", "User", new {Area = "Security"});
                        }
                      
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                   

                       
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
