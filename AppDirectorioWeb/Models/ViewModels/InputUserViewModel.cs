using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class InputUserViewModel
    {
        [Required(ErrorMessage = "El nombre completo es requerido")]
        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email no valido")]
        [Display(Name = "Email")]

        public string Email { get; set; }

        [Required(ErrorMessage = "El número de telefono es requerido")]
        [Phone(ErrorMessage = "télefono no valido")]
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
    }
}
