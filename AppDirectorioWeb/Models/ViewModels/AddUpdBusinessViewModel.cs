using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class AddUpdBusinessViewModel
    {

        public BussinesViewModel Business { get; set; }
        public InputUserViewModel User { get; set; }
        public List<FeatureNegocioViewModel> FeatureNegocios { get; set; }
        public List<HorarioNegocioViewModel> HorarioNegocios { get; set; }
        public List<ImagenesNegocioViewModel> ImagenesNegocios { get; set; }
    }
}
