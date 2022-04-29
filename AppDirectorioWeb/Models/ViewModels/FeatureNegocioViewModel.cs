using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class FeatureNegocioViewModel
    {
        public long Id { get; set; }
        public int? IdFeature { get; set; }
        public int? IdNegocio { get; set; }
        public bool? Activo { get; set; }
        public string IdUserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string IdUserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<CatCategoryViewModel> Features { get; set; }
    }
}
