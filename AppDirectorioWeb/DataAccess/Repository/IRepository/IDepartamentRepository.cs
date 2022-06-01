using DataAccess.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IDepartamentRepository : IRepository<CatDepartamento>
    {
        #region Public Methods

        void Update(CatDepartamento department);

        #endregion Public Methods
    }
}