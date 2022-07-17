using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class CatPlan
    {
        public CatPlan()
        {
            UserDetails = new HashSet<UserDetail>();
        }

        public int Id { get; set; }
        public string PlanName { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<UserDetail> UserDetails { get; set; }
    }
}
