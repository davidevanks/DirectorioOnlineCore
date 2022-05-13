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
            var roles = _db.Roles.AsQueryable();
            var roleUser = _db.UserRoles.AsQueryable();
            var user = _db.Users.AsQueryable();
            var userDetail = _db.UserDetails.AsQueryable();

            var query = (from u in user
                         join ud in userDetail on u.Id equals ud.UserId into uddef
                         from udff in uddef.DefaultIfEmpty()
                         join ur in roleUser on u.Id equals ur.UserId
                         join r in roles on ur.RoleId equals r.Id
                         join urr in user on udff.IdUserCreate equals urr.Id into urrdef
                         from urrdf in urrdef.DefaultIfEmpty()
                         join urup in user on udff.IdUserUpdate equals urup.Id into urupdef
                         from urupdf in urupdef.DefaultIfEmpty()
                         select new UserViewModel
                         {
                             Id=u.Id,
                             Email=u.Email,
                             PhoneNumber=u.PhoneNumber,
                             FullName= udff.FullName==null?"": udff.FullName,
                             Role=r.Name,
                             ProfilePicture= udff.UserPicture,
                             UserRegistration= urrdf.UserName,
                             RegistrationDate= udff.RegistrationDate,
                             UpdateUser= urupdf.UserName,
                             NotificationsPromo= udff.NotificationsPromo==null?false: (bool)udff.NotificationsPromo,
                             LockoutEnd=u.LockoutEnd
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
