using System;
using System.Collections.Generic;

#nullable disable

namespace DataAccess.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public DateTime? RegistrationDate { get; set; }
    }
}
