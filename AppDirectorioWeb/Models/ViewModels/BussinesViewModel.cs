using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Models.ViewModels
{
    public class BussinesViewModel
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "El Nombre del negocio es requerido")]
        [Display(Name = "Nombre Negocio")]
        public string NombreNegocio { get; set; }
        [Required(ErrorMessage = "La descripción del negocio es requerida")]
        [Display(Name = "Descripción Negocio")]
        public string DescripcionNegocio { get; set; }
        [Required(ErrorMessage = "Los Tags son requeridos")]
        [Display(Name = "Tags")]
        public string Tags { get; set; }
        [Required(ErrorMessage = "La categoría es requerida")]
        [Display(Name = "Categoría")]
        public string IdCategoria { get; set; }
        [Required(ErrorMessage = "El departamento es requerido")]
        [Display(Name = "Departamento")]
        public string IdDepartamento { get; set; }
        [Required(ErrorMessage = "La dirección del negocio es requerida")]
        [Display(Name = "Dirección Negocio")]
        public string DireccionNegocio { get; set; }
        [Required(ErrorMessage = "El número de telefono es requerido")]
        [Phone(ErrorMessage = "télefono no valido")]
        [Display(Name = "Número de telefono")]
        public string TelefonoNegocio1 { get; set; }
        public string TelefonoNegocio2 { get; set; }
        [Required(ErrorMessage = "Número de whatsApp es requerido")]
        [Phone(ErrorMessage = "télefono no valido")]
        [Display(Name = "Número de WhatsApp")]
        public string TelefonoWhatsApp { get; set; }
        [Required(ErrorMessage = "El email es requerido")]
        [EmailAddress(ErrorMessage = "Email no valido")]
        [Display(Name = "Email Negocio")]
        public string EmailNegocio { get; set; }
        [Display(Name = "Sitio Web")]
        [Url(ErrorMessage ="Sitio web no valido")]
        public string SitioWebNegocio { get; set; }
        [Display(Name = "Linkedin")]
        [Url(ErrorMessage = "Linkedin no valido")]
        public string LinkedInUrl { get; set; }
        [Display(Name = "Facebook")]
        [Url(ErrorMessage = "Facebook no valido")]
        public string FacebookUrl { get; set; }
        [Display(Name = "Instagram")]
        [Url(ErrorMessage = "Instagram no valido")]
        public string InstagramUrl { get; set; }
        [Display(Name = "Twitter")]
        [Url(ErrorMessage = "Twitter no valido")]
        public string TwitterUrl { get; set; }
        public bool HasDelivery { get; set; }
        public bool PedidosYa { get; set; }
        public bool Hugo { get; set; }
        public bool Piki { get; set; }
        public string LogoNegocio { get; set; }
        public int? Status { get; set; }
        public string IdUserCreate { get; set; }
        public DateTime? CreateDate { get; set; }
        public string IdUserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    
        //public virtual ICollection<ReviewViewModel> Reviews { get; set; }
    }
}
