using DataAccess.Models;
using Models.ViewModels;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface IUserDetailsRepository : IRepository<UserDetail>
    {
        #region Public Methods

        List<UserViewModel> GetAUsersDetails(string userId);

        void Update(UserViewModel userProfile);

        void UpdateProfilePicture(UserViewModel userProfile);

        #endregion Public Methods
    }
}