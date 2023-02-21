using System;
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class FacturaViewModel
    {
        #region Public Properties
        public string UserEmail { get; set; }
        public bool FacturaEnviada { get; set; }
        public bool FacturaPagada { get; set; }
        public string FechaPago { get; set; }
        [Display(Name = "No. Factura")]
        public int IdFactura { get; set; }
        public int IdPlan { get; set; }
        [Required(ErrorMessage = "No. Autorización requerido")]
        [Display(Name = "No. Autorización")]
        public string NoAutorizacion { get; set; }
        [Display(Name = "Suscripción")]
        public string PlanSuscripcion { get; set; }
        [Required(ErrorMessage = "contraseña actual es requerida")]
        [DataType(DataType.Password)]
        [Display(Name = "Usuario")]
        public string User { get; set; }
        public string UserId { get; set; }
        public string MontoPago { get; set; }

        #endregion Public Properties
    }
}