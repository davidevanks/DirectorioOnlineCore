using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class UserDetailRepository : Repository<UserDetail>, IUserDetailsRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public UserDetailRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
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
