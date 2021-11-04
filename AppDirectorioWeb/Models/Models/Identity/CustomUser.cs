using Identity.Dapper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Models.Models.Identity
{
    public class CustomUser : DapperIdentityUser
    {
        public string Address { get; set; }

        public CustomUser() { }
        public CustomUser(string userName) : base(userName) { }
    }
}