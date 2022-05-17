using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class BusinessSearchResult
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessDescription { get; set; }
        public string DepartmentName { get; set; }
        public string CategoryName { get; set; }
        public string BusinessLogo { get; set; }
        public int BusinessStars { get; set; }
    }
}
