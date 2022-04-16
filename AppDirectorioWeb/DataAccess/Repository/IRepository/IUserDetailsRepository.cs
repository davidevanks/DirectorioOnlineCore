using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IUserDetailsRepository : IRepository<UserDetail>
    {
        void Update(UserDetail userDetail);
    }
}
