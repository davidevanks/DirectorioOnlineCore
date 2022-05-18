using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IReviewRepository : IRepository<Review>
    {
        List<ReviewViewModel> GetReviewsByBusinessId(int businessId);
    }
}
