using Microsoft.AspNetCore.Http;
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

        public IFormFile Logo { get; set; }
        public List<IFormFile> PicturesBusiness { get; set; }
    }
}
