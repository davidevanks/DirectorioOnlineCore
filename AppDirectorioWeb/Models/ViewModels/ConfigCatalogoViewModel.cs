using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ConfigCatalogoViewModel
    {
        public int Id { get; set; }
        public int IdNegocio { get; set; }
        public string NombreNegocio { get; set; }
        [Required(ErrorMessage = "El nombre del cátalogo es requerido")]
        [Display(Name = "Nombre catálogo", Prompt = "Nombre del catálogo")]
        public string NombreCatalogo { get; set; }
        [Required(ErrorMessage = "El tipo cátalogo es requerido")]
        [Display(Name = "Tipo catálogo", Prompt = "Tipo del catálogo")]
        public int IdTipoCatalogo { get; set; }//1 productos, 2 servicio
        [Display(Name = "Tipo catálogo", Prompt = "Tipo del catálogo")]
        public string NombreTipoCatalogo { get; set; }
        [Required(ErrorMessage = "El tipo moneda es requerido")]
        public int IdMoneda { get; set; } //1 cordobas, 2 dólares
        [Display(Name = "Móneda", Prompt = "Móneda")]
        public string NombreMoneda { get; set; }
        [Display(Name = "Aplicar descuento másivo?", Prompt = "Aplicar descuento másivo?")]
        public bool DescuentoMasivo { get; set; }

        public string NombreDescuentoMasivo { get; set; }
        [Display(Name = "Cuál es el porcentaje del descuento másivo?", Prompt = "Cuál es el porcentaje del descuento másivo?")]
        public int? PorcentajeDescuentoMasivo { get; set; }
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        public string DescripcionActivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string IdUsuarioActualizacion { get; set; }

        public List<CatTipoPagoXcatalogoConfigViewModel> lstTipoPagos { get; set; }
        public string NombreTipoPagos { get; set; }
    }
}
