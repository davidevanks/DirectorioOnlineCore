using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class CatCategorium
    {
        public CatCategorium()
        {
            FeatureNegocios = new HashSet<FeatureNegocio>();
            ItemCatalogos = new HashSet<ItemCatalogo>();
            Negocios = new HashSet<Negocio>();
        }

        public int Id { get; set; }
        public int? IdPadre { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }
        public string IdUserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string IdUserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FeatureNegocio> FeatureNegocios { get; set; }
        public virtual ICollection<ItemCatalogo> ItemCatalogos { get; set; }
        public virtual ICollection<Negocio> Negocios { get; set; }
    }
}
