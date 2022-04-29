using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;


namespace Models.ViewModels
{
    public class UserViewModel: IdentityUser
    {
        public string Role { get; set; }

        public string RoleId { get; set; }
        public string FullName { get; set; }
        public string ProfilePicture { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string UserRegistration { get; set; }
        public bool? NotificationsPromo { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string UpdateUser { get; set; }
    }
}
