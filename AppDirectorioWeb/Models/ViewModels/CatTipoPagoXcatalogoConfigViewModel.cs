using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CatTipoPagoXcatalogoConfigViewModel
    {
        public int Id { get; set; }
        public int? IdCatConfigProdServ { get; set; }
        public string NombreTipoPago { get; set; }
        public int IdTipoPago { get; set; }
        public bool Active { get; set; }
    }
}
