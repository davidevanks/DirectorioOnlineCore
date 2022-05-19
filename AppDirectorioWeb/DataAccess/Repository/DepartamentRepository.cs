using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System.Linq;

namespace DataAccess.Repository
{
    public class DepartamentRepository : Repository<CatDepartamento>, IDepartamentRepository
    {
        #region Private Fields

        private readonly DirectorioOnlineCoreContext _db;

        #endregion Private Fields

        #region Public Constructors

        public DepartamentRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        public void Update(CatDepartamento department)
        {
            var objFromDb = _db.CatDepartamentos.FirstOrDefault(s => s.Id == department.Id);
            if (objFromDb != null)
            {
                objFromDb.Nombre = department.Nombre;
                objFromDb.Activo = department.Activo;
                objFromDb.IdUserUpdate = department.IdUserUpdate;
                objFromDb.UpdateDate = department.UpdateDate;
                objFromDb.IdPais = department.IdPais;
            }
        }

        #endregion Public Methods
    }
}