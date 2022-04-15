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
    }
}
