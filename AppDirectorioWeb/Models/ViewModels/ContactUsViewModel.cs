using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ContactUsViewModel
    {
        [Required(ErrorMessage = "El asunto es requerido")]
        [Display(Name = "Asunto")]
        public string Subject { get; set; }
       
        [Display(Name = "Nombre Compañia")]
        public string CompanyName { get; set; }
        [Display(Name = "Nombre Completo")]
        [Required(ErrorMessage = "El Nombre Completo es requerido")]
        public string PersonName { get; set; }
       
        [Required(ErrorMessage = "El número de telefono es requerido")]
        [Phone(ErrorMessage = "télefono no valido")]
        [Display(Name = "Número de telefono", Prompt = "7777777")]
        public string Phone { get; set; }
        
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email no valido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El Mensaje es requerido")]
        [Display(Name = "Mensaje")]
        public string Message { get; set; }


    }
}
