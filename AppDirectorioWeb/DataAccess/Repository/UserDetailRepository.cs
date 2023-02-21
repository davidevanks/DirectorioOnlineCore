using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class UserDetailRepository : Repository<UserDetail>, IUserDetailsRepository
    {
        #region Private Fields

        private readonly DirectorioOnlineCoreContext _db;

        #endregion Private Fields

        #region Public Constructors

        public UserDetailRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        public List<UserViewModel> GetAUsersDetails(string userId)
        {
            var roles = _db.Roles.AsQueryable();
            var roleUser = _db.UserRoles.AsQueryable();
            var user = _db.Users.AsQueryable();
            var userDetail = _db.UserDetails.AsQueryable();
            var facturas = _db.Facturas.AsQueryable();
            var catPlan = _db.CatPlans.AsQueryable();

            var query = (from u in user
                         join ud in userDetail on u.Id equals ud.UserId into uddef
                         from udff in uddef.DefaultIfEmpty()
                         join ur in roleUser on u.Id equals ur.UserId
                         join r in roles on ur.RoleId equals r.Id
                         join urr in user on udff.IdUserCreate equals urr.Id into urrdef
                         from urrdf in urrdef.DefaultIfEmpty()
                         join urup in user on udff.IdUserUpdate equals urup.Id into urupdef
                         from urupdf in urupdef.DefaultIfEmpty()
                         join plan in catPlan on udff.IdPlan equals plan.Id
                         select new UserViewModel
                         {
                             Id = u.Id,
                             Email = u.Email,
                             PhoneNumber = u.PhoneNumber,
                             FullName = udff.FullName == null ? "" : udff.FullName,
                             Role = r.Name,
                             ProfilePicture = udff.UserPicture,
                             UserRegistration = urrdf.UserName,
                             RegistrationDate = udff.RegistrationDate,
                             UpdateUser = urupdf.UserName,
                             NotificationsPromo = udff.NotificationsPromo == null ? false : (bool)udff.NotificationsPromo,
                             LockoutEnd = u.LockoutEnd,
                             UserName = u.UserName,
                             Subscripcion = plan.PlanName,
                             IdPlan= udff.IdPlan,
                             PlanExpirationDate = (udff.PlanExpirationDate!=null)? Convert.ToDateTime(udff.PlanExpirationDate).ToShortDateString():""
                         }) ;
            
            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x => x.Id == userId);
            }

            return query.ToList();
        }

        public void Update(UserViewModel userProfile)
        {
            var objFromDb = _db.UserDetails.FirstOrDefault(s => s.UserId == userProfile.Id);
            if (objFromDb != null)
            {
                objFromDb.FullName = userProfile.FullName;
                objFromDb.UpdateDate = userProfile.UpdateDate;
                objFromDb.NotificationsPromo = userProfile.NotificationsPromo;
                objFromDb.IdUserUpdate = userProfile.UpdateUser;
            }

            var objFromDbUser = _db.Users.FirstOrDefault(s => s.Id == userProfile.Id);
            if (objFromDbUser != null)
            {
                objFromDbUser.PhoneNumber = userProfile.PhoneNumber;
                objFromDbUser.Email = userProfile.Email;
                objFromDbUser.UserName = userProfile.UserName;
            }
        }

        public void UpdatePlanSuscripcionUser(UserViewModel userProfile)
        {
            var objFromDb = _db.UserDetails.FirstOrDefault(s => s.UserId == userProfile.Id);
            if (objFromDb != null)
            {
                objFromDb.IdPlan = userProfile.IdPlan;
                objFromDb.PlanExpirationDate = userProfile.PlanExpirationDateD;
            }
        }

        public void UpdateProfilePicture(UserViewModel userProfile)
        {
            var objFromDb = _db.UserDetails.FirstOrDefault(s => s.UserId == userProfile.Id);
            if (objFromDb != null)
            {
                objFromDb.UserPicture = userProfile.ProfilePicture;
            }
        }

        public int VerifyUserIdPlan(string userId)
        {
            return (int)_db.UserDetails.Where(x=>x.UserId==userId).FirstOrDefault().IdPlan;
        }

        #endregion Public Methods
    }
}