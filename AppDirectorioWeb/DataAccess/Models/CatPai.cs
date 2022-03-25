using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class CatPai
    {
        public CatPai()
        {
            CatDepartamentos = new HashSet<CatDepartamento>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public bool? Activo { get; set; }

        public virtual ICollection<CatDepartamento> CatDepartamentos { get; set; }
    }
}
