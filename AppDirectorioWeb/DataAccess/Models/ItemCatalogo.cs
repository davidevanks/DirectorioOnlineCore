using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class ItemCatalogo
    {
        public int Id { get; set; }
        public int IdConfigCatalogo { get; set; }
        public int IdCategoriaItem { get; set; }
        public string NombreItem { get; set; }
        public string DescripcionItem { get; set; }
        public decimal PrecioUnitario { get; set; }
        public bool TieneDescuento { get; set; }
        public int PorcentajeDescuento { get; set; }
        public string ImagenItem { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string IdUsuarioCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string IdUsuarioActualizacion { get; set; }

        public virtual CatCategorium IdCategoriaItemNavigation { get; set; }
        public virtual ConfigCatalogo IdConfigCatalogoNavigation { get; set; }
    }
}
