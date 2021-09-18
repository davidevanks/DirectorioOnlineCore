using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models.Identity.ManageViewModels
{
    public class AddRoleViewModel
    {
        [Display(Name = "Role Name")]
        [Required]
        public string RoleName { get; set; }
    }
}
