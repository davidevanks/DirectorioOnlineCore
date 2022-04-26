using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class BussinesViewModel
    {
        public int Id { get; set; }
        public string NombreNegocio { get; set; }
        public string DescripcionNegocio { get; set; }
        public string Tags { get; set; }
        public int IdCategoria { get; set; }
        public int IdDepartamento { get; set; }
        public string DireccionNegocio { get; set; }
        public string TelefonoNegocio1 { get; set; }
        public string TelefonoNegocio2 { get; set; }
        public string TelefonoWhatsApp { get; set; }
        public string EmailNegocio { get; set; }
        public string SitioWebNegocio { get; set; }
        public string LinkedInUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }
        public string TwitterUrl { get; set; }
        public bool? HasDelivery { get; set; }
        public bool? PedidosYa { get; set; }
        public bool? Hugo { get; set; }
        public bool? Piki { get; set; }
        public byte[] LogoNegocio { get; set; }
        public int Status { get; set; }
        public string IdUserCreate { get; set; }
        public DateTime CreateDate { get; set; }
        public string IdUserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual CatCategoryViewModel IdCategoriaNavigation { get; set; }
        public virtual CatDepartamentoViewModel IdDepartamentoNavigation { get; set; }
        public virtual ICollection<FeatureNegocioViewModel> FeatureNegocios { get; set; }
        public virtual ICollection<HorarioNegocioViewModel> HorarioNegocios { get; set; }
        public virtual ICollection<ImagenesNegocioViewModel> ImagenesNegocios { get; set; }
        public virtual ICollection<ReviewViewModel> Reviews { get; set; }
    }
}
