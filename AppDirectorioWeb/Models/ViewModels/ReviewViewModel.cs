using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ReviewViewModel
    {
        public long Id { get; set; }
        public int IdBusiness { get; set; }
        public string IdUser { get; set; }
        public string Comments { get; set; }
        public int Stars { get; set; }
        public bool Active { get; set; }
        public string UserNameComments { get; set; }
        public string PictureUser { get; set; }
    }
}
