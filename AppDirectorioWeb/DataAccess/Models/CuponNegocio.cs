using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class CuponNegocio
    {
        public CuponNegocio()
        {
            CuponRedencionUsuarios = new HashSet<CuponRedencionUsuario>();
        }

        public int Id { get; set; }
        public int IdNegocio { get; set; }
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

        public virtual ICollection<CuponRedencionUsuario> CuponRedencionUsuarios { get; set; }
    }
}
