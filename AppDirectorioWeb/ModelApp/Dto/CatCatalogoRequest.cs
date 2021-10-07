using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApp.Dto
{
    public class CatCatalogoRequest
    {
        public int Id { get; set; }
        public int IdPadre { get; set; }
        public string Nombre { get; set; }

        public bool Activo { get; set; }
    }
}
