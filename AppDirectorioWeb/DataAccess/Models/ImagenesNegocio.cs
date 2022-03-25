using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class ImagenesNegocio
    {
        public long Id { get; set; }
        public int? IdNegocio { get; set; }
        public byte[] Image { get; set; }

        public virtual Negocio IdNegocioNavigation { get; set; }
    }
}
