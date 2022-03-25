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
            Negocios = new HashSet<Negocio>();
        }

        public int Id { get; set; }
        public int? IdPadre { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<FeatureNegocio> FeatureNegocios { get; set; }
        public virtual ICollection<Negocio> Negocios { get; set; }
    }
}
