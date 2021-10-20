using System;
using System.Collections.Generic;
using System.Text;

namespace ModelApp.Dto.FotosAnuncio
{
	public class FotosAnuncioConsultarDto
	{
		public long Id { get; set; }
		public long IdNegocio { get; set; }
		public DateTime FechaCreacion { get; set; }
		public DateTime FechaModificacion { get; set; }
		public long IdUsuarioModificacion { get; set; }
		public long IdUsuarioCreacion { get; set; }
		public string Foto { get; set; }
		public bool Activo { get; set; }
	}
}
