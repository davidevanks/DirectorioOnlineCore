using System;
using System.Collections.Generic;
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
        public string DescripcionPromocion { get; set; }
        public bool DescuentoPorcentaje { get; set; }
        public bool DescuentoMonto { get; set; }
        public int? MonedaMonto { get; set; }
        public decimal ValorCupon { get; set; }
        public int CantidadCuponDisponible { get; set; }
        public int CantidadCuponUsados { get; set; }
        public string ImagenCupon { get; set; }
        public DateTime FechaExpiracionCupon { get; set; }
        public bool Status { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string IdUsuarioModificacion { get; set; }
    }
}
