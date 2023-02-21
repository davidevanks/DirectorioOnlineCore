using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Factura
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int IdPlan { get; set; }
        public string NoAutorizacionPago { get; set; }
        public decimal MontoPagado { get; set; }
        public DateTime? FechaPago { get; set; }
        public bool FacturaPagada { get; set; }
        public bool FacturaEnviada { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public string IdUserCreate { get; set; }
        public string IdUserUpdate { get; set; }
    }
}
