using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
   public  class HorarioNegocioViewModel
    {
        public long Id { get; set; }
        public int? IdNegocio { get; set; }
        public int? IdDia { get; set; }
        public string HoraDesde { get; set; }
        public string HoraHasta { get; set; }
        public string IdUserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string IdUserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
