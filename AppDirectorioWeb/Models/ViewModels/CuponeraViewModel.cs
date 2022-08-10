using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CuponeraViewModel
    {
        public int Id { get; set; }
        public int IdNegocio { get; set; }
        public string NombreNegocio { get; set; }
        public int? IdUsuarioRedencion { get; set; }
        public string NombreUsuarioRedencion { get; set; }
        [DisplayName("Descripción cupón")]
        [Required(ErrorMessage = "Campo requerido")]
        public string DescripcionPromocion { get; set; }
        [DisplayName("Porcentaje de descuento")]
        [Required(ErrorMessage = "Campo requerido")]
        public bool DescuentoPorcentaje { get; set; }
        [DisplayName("Monto en efectivo de descuento")]
        [Required(ErrorMessage = "Campo requerido")]
        public bool DescuentoMonto { get; set; }
        [DisplayName("Moneda cupón")]
        public int? MonedaMonto { get; set; }
        [DisplayName("Valor cupón")]
        [Required(ErrorMessage = "Campo requerido")]
        public decimal ValorCupon { get; set; }
        [DisplayName("Cantidad de cupones disponibles")]
        [Required(ErrorMessage = "Campo requerido")]
        public int CantidadCuponDisponible { get; set; }
        public int? CantidadCuponUsados { get; set; }
        public string ImagenCupon { get; set; }
        [DisplayName("Fecha de expiración del cupón")]
        [Required(ErrorMessage = "Campo requerido")]
        public string FechaExpiracionCupon { get; set; }
        [DisplayName("Estado cupón")]
        [Required(ErrorMessage = "Campo requerido")]
        public bool Status { get; set; }
        public IFormFile PictureCupon { get; set; }

        public DateTime FechaCreacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
    }
}
