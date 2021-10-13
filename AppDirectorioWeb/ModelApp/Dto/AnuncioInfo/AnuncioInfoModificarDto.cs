using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApp.Dto.AnuncioInfo
{
    public class AnuncioInfoModificarDto
    {
		public string NombreNegocio { get; set; }
		public int IdCategoria { get; set; }
		public string DescripcionNegocio { get; set; }
		public int IdPais { get; set; }
		public int IdDepartamento { get; set; }
		public string DireccionNegocio { get; set; }
		public int Longitud { get; set; }
		public int Latitud { get; set; }
		public string TelefonoNegocio { get; set; }
		public string EmailNegocio { get; set; }
		public string PaginaWebNegocio { get; set; }
		public string InstagramNegocio { get; set; }
		public string FacebookNegocio { get; set; }
		public string TwitterNegocio { get; set; }
		public string WhatsApp { get; set; }
		public bool TieneDelivery { get; set; }
		public bool Hugo { get; set; }
		public bool PedidosYa { get; set; }
		public bool Piki { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime FechaModificacion { get; set; }
		public long IdUsuarioCreacion { get; set; }
		public long IdUsuarioModificacion { get; set; }
		public long Id { get; set; }
		public bool HabilitarHorarioFeriado { get; set; }
		public int Calificacion { get; set; }
		public string LogoNegocio { get; set; }
		public long Idusuario { get; set; }
		public bool Activo { get; set; }
	}
}
