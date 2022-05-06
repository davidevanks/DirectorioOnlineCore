using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class CatCategoryViewModel
    {
        public int Id { get; set; }
        public int? IdPadre { get; set; }
        [DisplayName("Nombre Categoria")]
        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
