using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<CatCategorium>
    {
        #region Public Methods

        IEnumerable<CatCategorium> GetCatUsedByBusiness();

        void Update(CatCategorium category);

        #endregion Public Methods
    }
}