using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class CuponRedencionUsuario
    {
        public int Id { get; set; }
        public string IdUsuario { get; set; }
        public int IdCupon { get; set; }
        public DateTime FechaRedencion { get; set; }

        public virtual CuponNegocio IdCuponNavigation { get; set; }
    }
}
