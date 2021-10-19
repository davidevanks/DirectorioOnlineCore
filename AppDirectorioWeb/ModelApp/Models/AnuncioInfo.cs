using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApp.Models
{
    public class AnuncioInfo
    {
		public string NombreNegocio { get; set; }
		public int IdCategoria { get; set; }
		public string DescripcionNegocio { get; set; }
		public int IdPais { get; set; }
		public int IdDepartamento { get; set; }
		public string DireccionNegocio { get; set; }
		
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

		public decimal Longitud { get; set; }
		public decimal Latitud { get; set; }
		public bool AbLunes { get; set; }
		public bool AbMartes { get; set; }
		public bool AbMiercoles { get; set; }
		public bool AbJueves { get; set; }
		public bool AbViernes { get; set; }
		public bool AbSabado { get; set; }
		public bool AbDomingo { get; set; }
		public DateTime ILunes { get; set; }
		public DateTime FLunes { get; set; }
		public DateTime IMartes { get; set; }
		public DateTime FMartes { get; set; }
		public DateTime IMiercoles { get; set; }
		public DateTime FMiercoles { get; set; }
		public DateTime IJueves { get; set; }
		public DateTime FJueves { get; set; }
		public DateTime IViernes { get; set; }
		public DateTime FViernes { get; set; }
		public DateTime ISabado { get; set; }
		public DateTime FSabado { get; set; }
		public DateTime IDomingo { get; set; }
		public DateTime FDomingo { get; set; }
		public bool EstaAtendiendo { get; set; }
	}
}
