using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class DetailsBusinessViewModel
    {
        public BusinessOwnerViewModel Business { get; set; }
        public List<FeatureNegocioViewModel> FeatureNegocios { get; set; }
        public List<HorarioNegocioViewModel> HorarioNegocios { get; set; }
        public List<ImagenesNegocioViewModel> ImagenesNegocios { get; set; }
        public CuponeraViewModel CuponNegocio { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}
