using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Negocio
    {
        public Negocio()
        {
            FeatureNegocios = new HashSet<FeatureNegocio>();
            HorarioNegocios = new HashSet<HorarioNegocio>();
            ImagenesNegocios = new HashSet<ImagenesNegocio>();
        }

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
        public string TwitterUrl { get; set; }
        public byte[] LogoNegocio { get; set; }
        public int Status { get; set; }

        public virtual CatCategorium IdCategoriaNavigation { get; set; }
        public virtual CatDepartamento IdDepartamentoNavigation { get; set; }
        public virtual ICollection<FeatureNegocio> FeatureNegocios { get; set; }
        public virtual ICollection<HorarioNegocio> HorarioNegocios { get; set; }
        public virtual ICollection<ImagenesNegocio> ImagenesNegocios { get; set; }
    }
}
