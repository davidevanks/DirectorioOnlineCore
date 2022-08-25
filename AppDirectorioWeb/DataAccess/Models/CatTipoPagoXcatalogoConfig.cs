using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class CatTipoPagoXcatalogoConfig
    {
        public int Id { get; set; }
        public int? IdCatConfigProdServ { get; set; }
        public int? IdTipoPago { get; set; }
        public bool? Active { get; set; }
    }
}
