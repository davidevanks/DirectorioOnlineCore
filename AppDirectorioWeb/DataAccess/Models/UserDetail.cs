using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserPicture { get; set; }
        public string FullName { get; set; }
        public bool? NotificationsPromo { get; set; }
        public int? IdPlan { get; set; }
        public DateTime? PlanExpirationDate { get; set; }
        public string IdUserCreate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string IdUserUpdate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual CatPlan IdPlanNavigation { get; set; }
    }
}
