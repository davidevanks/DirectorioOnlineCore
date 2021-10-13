using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppDirectorioWeb.Models.Identity.AccountViewModels
{
    public class TokenViewModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
