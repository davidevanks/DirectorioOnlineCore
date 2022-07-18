using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;


namespace Models.ViewModels
{
    public class UserViewModel: IdentityUser
    {
        public string Role { get; set; }

        public string RoleId { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string UserRegistration { get; set; }
        public bool NotificationsPromo { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUser { get; set; }
        public string Subscripcion { get; set; }
      
        public string PlanExpirationDate { get; set; }

       

        public ChangePassowrdViewModel ChangePassword { get; set; }

        public IFormFile Picture { get; set; }
    }

    public class ChangePassowrdViewModel
    {
        [Required(ErrorMessage = "contraseña actual es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Nueva contraseña es requerida")]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} y un máximo de {1} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "La nueva contraseña y la confirmación de contraseña no coiciden.")]
        public string ConfirmPassword { get; set; }
    }
}
