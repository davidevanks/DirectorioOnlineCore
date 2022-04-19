using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;

namespace DataAccess.Repository
{
    public class UserDetailRepository : Repository<UserDetail>, IUserDetailsRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public UserDetailRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        public List<UserViewModel> GetAUsersDetails(string userId)
        {
            var roles = _db.Roles;
            var roleUser = _db.UserRoles;
            var user = _db.Users;
            var userDetail = _db.UserDetails;

            var query = (from u in user
                         join ud in userDetail on u.Id equals ud.UserId
                         join ur in roleUser on u.Id equals ur.UserId
                         join r in roles on ur.RoleId equals r.Id
                         join urr in user on ud.IdUserCreate equals urr.Id
                         join urup in user on ud.IdUserUpdate equals urup.Id into urupdef
                         from urupdf in urupdef.DefaultIfEmpty()
                         select new UserViewModel
                         {
                             Id=u.Id,
                             Email=u.Email,
                             PhoneNumber=u.PhoneNumber,
                             FullName=ud.FullName,
                             Role=r.Name,
                             ProfilePicture=ud.UserPicture,
                             UserRegistration= urr.UserName,
                             RegistrationDate=ud.RegistrationDate,
                             UpdateUser= urupdf.UserName,
                             NotificationsPromo=ud.NotificationsPromo
                         });


            if (!string.IsNullOrEmpty(userId))
            {
                query=query.Where(x=>x.Id==userId);
            }

            return query.ToList();


        }

        public void Update(UserDetail userDetail)
        {
            var objFromDb = _db.UserDetails.FirstOrDefault(s => s.UserId == userDetail.UserId);
            if (objFromDb != null)
            {
                objFromDb.FullName = userDetail.FullName;
                objFromDb.RegistrationDate = userDetail.RegistrationDate;

            }


        }
    }
}
