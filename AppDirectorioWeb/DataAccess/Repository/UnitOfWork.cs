using DataAccess.Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Private Fields

        private readonly DirectorioOnlineCoreContext _db;

        #endregion Private Fields

        #region Public Constructors

        public UnitOfWork(DirectorioOnlineCoreContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            SP_CALL = new SP_CALL(_db);
            UserDetail = new UserDetailRepository(_db);
            Departament = new DepartamentRepository(_db);
            Business = new BusinessRepository(_db);
            Feature = new FeatureNegocioRepository(_db);
            ScheduleBusiness = new ScheduleBusinessRepository(_db);
            ImageBusiness = new ImagesBusinessRepository(_db);
            Review = new ReviewRepository(_db);
            Factura = new FacturaRepository(_db);
        }

        #endregion Public Constructors

        #region Public Properties

        public IBusinessRepository Business { get; private set; }
        public ICategoryRepository Category { get; private set; }
        public IDepartamentRepository Departament { get; private set; }
        public IFeatureNegocioRepository Feature { get; private set; }
        public IImagesBusinessRepository ImageBusiness { get; private set; }
        public IReviewRepository Review { get; private set; }
        public IScheduleBusinessRepository ScheduleBusiness { get; private set; }
        public ISP_Call SP_CALL { get; private set; }
        public IUserDetailsRepository UserDetail { get; private set; }
        public IFacturaRepository Factura { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        #endregion Public Methods
    }
}