using System;

namespace Models.ViewModels
{
    public class FacturaViewModel
    {
        #region Public Properties

        public bool FacturaEnviada { get; set; }
        public bool FacturaPagada { get; set; }
        public DateTime? FechaPago { get; set; }
        public int IdFactura { get; set; }
        public int IdPlan { get; set; }
        public string NoAutorizacion { get; set; }
        public string PlanSuscripcion { get; set; }
        public string User { get; set; }
        public string UserId { get; set; }

        #endregion Public Properties
    }
}