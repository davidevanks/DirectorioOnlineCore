using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class LookForBusinessViewModel
    {
        public string Search { get; set; }
        public string IdCategoria { get; set; }
        public string IdDepartamento { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
        public IEnumerable<SelectListItem> Departamentos { get; set; }
    }
}
