using System;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        #region Public Properties

        public IBusinessRepository Business { get; }
        ICategoryRepository Category { get; }
        public IDepartamentRepository Departament { get; }
        public IFeatureNegocioRepository Feature { get; }
        public IImagesBusinessRepository ImageBusiness { get; }
        public IReviewRepository Review { get; }
        public IScheduleBusinessRepository ScheduleBusiness { get; }
        ISP_Call SP_CALL { get; }

        public IUserDetailsRepository UserDetail { get; }

        public IFacturaRepository Factura { get; }
        public ICuponeraRepository Cuponera { get; }

        #endregion Public Properties

        #region Public Methods

        void Save();

        #endregion Public Methods
    }
}