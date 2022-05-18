using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ReviewRepository : Repository<Review>, IReviewRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public ReviewRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }
        public List<ReviewViewModel> GetReviewsByBusinessId(int businessId)
        {
            var users = _db.Users.AsQueryable();
            var reviews = _db.Reviews.AsQueryable();
            var usersDetails = _db.UserDetails.AsQueryable();

            var query = (from rev in reviews join u in users on rev.IdUser equals u.Id into userdef
                         from udef in userdef.DefaultIfEmpty()
                         join ud in usersDetails on udef.Id equals ud.UserId into userdetdef
                         from userdetdff in userdetdef.DefaultIfEmpty()
                         where rev.Active==true && rev.IdBusiness==businessId
                         select new ReviewViewModel { 
                         Id=rev.Id,
                         IdBusiness=rev.IdBusiness,
                         IdUser= string.IsNullOrEmpty(udef.Id)==true?"0": udef.Id,
                         UserNameComments= string.IsNullOrEmpty(userdetdff.FullName) == true ? rev.FullName : userdetdff.FullName,
                         Stars=rev.Stars,
                         PictureUser= string.IsNullOrEmpty(userdetdff.UserPicture) == true? "avatar-01.png": userdetdff.UserPicture,
                         Comments=rev.Comments
                         }).ToList();

            return query;
        }
    }
}
