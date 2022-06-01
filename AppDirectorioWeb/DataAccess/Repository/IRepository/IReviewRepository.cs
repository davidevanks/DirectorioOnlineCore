using DataAccess.Models;
using Models.ViewModels;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        #region Public Methods

        List<ReviewViewModel> GetReviewsByBusinessId(int businessId);

        #endregion Public Methods
    }
}