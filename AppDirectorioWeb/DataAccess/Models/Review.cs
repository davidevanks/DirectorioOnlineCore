using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class Review
    {
        public long Id { get; set; }
        public int IdBusiness { get; set; }
        public string IdUser { get; set; }
        public string FullName { get; set; }
        public string Comments { get; set; }
        public string EmailComment { get; set; }
        public int Stars { get; set; }
        public bool Active { get; set; }

        public virtual Negocio IdBusinessNavigation { get; set; }
    }
}
