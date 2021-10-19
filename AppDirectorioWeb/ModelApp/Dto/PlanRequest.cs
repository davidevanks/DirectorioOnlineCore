using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelApp.Dto
{
   public class PlanRequest
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool showweb { get; set; }
    }
}
