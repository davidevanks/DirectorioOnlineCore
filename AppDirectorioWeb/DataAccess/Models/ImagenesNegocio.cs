using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class ImagenesNegocio
    {
        public long Id { get; set; }
        public int? IdNegocio { get; set; }
        public string Image { get; set; }
        public string IdUserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string IdUserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual Negocio IdNegocioNavigation { get; set; }
    }
}
