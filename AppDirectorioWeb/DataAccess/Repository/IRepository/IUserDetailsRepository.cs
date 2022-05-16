using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using Models.ViewModels;

namespace DataAccess.Repository.IRepository
{
    public interface IUserDetailsRepository : IRepository<UserDetail>
    {
        void Update(UserViewModel userProfile);
        List<UserViewModel> GetAUsersDetails(string userId);

        void UpdateProfilePicture(UserViewModel userProfile);
    }
}
