using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repository
{
    public class CategoryRepository : Repository<CatCategorium>, ICategoryRepository
    {
        #region Private Fields

        private readonly DirectorioOnlineCoreContext _db;

        #endregion Private Fields

        #region Public Constructors

        public CategoryRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        #endregion Public Constructors

        #region Public Methods

        public IEnumerable<CatCategorium> GetCatUsedByBusiness()
        {
            var cat = _db.CatCategoria.AsQueryable();
            var bus = _db.Negocios.AsQueryable();

            var query = (from c in cat
                         join b in bus on c.Id equals b.IdCategoria
                         where b.Status == 17
                         select new CatCategorium
                         {
                             Id = c.Id,
                             IdPadre = c.IdPadre,
                             Nombre = c.Nombre,
                             Activo = c.Activo,
                             IdUserCreate = c.IdUserCreate,
                             CreateDate = c.CreateDate,
                             IdUserUpdate = c.IdUserUpdate,
                             UpdateDate = c.UpdateDate
                         }).ToList();

            return query;
        }

        public void Update(CatCategorium category)
        {
            var objFromDb = _db.CatCategoria.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb != null)
            {
                objFromDb.Nombre = category.Nombre;
                objFromDb.Activo = category.Activo;
                objFromDb.IdUserUpdate = category.IdUserUpdate;
                objFromDb.UpdateDate = category.UpdateDate;
            }
        }

        #endregion Public Methods
    }
}