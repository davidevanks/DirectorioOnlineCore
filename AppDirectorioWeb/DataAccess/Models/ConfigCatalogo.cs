using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class ConfigCatalogo
    {
        public ConfigCatalogo()
        {
            ItemCatalogos = new HashSet<ItemCatalogo>();
        }

        public int Id { get; set; }
        public int IdNegocio { get; set; }
        public string NombreCatalogo { get; set; }
        public int? IdTipoCatalogo { get; set; }
        public int IdMoneda { get; set; }
        public int IdTipoPago { get; set; }
        public bool? DescuentoMasivo { get; set; }
        public int? PorcentajeDescuentoMasivo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string IdUsuarioActualizacion { get; set; }

        public virtual ICollection<ItemCatalogo> ItemCatalogos { get; set; }
    }
}
