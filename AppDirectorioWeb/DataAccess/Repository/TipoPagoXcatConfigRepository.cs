using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class TipoPagoXcatConfigRepository : Repository<CatTipoPagoXcatalogoConfig>, ITipoPagoXcatConfigRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public TipoPagoXcatConfigRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CatTipoPagoXcatalogoConfig model)
        {
            var objFromDb = _db.CatTipoPagoXcatalogoConfigs.FirstOrDefault(s => s.Id == model.Id);
            if (objFromDb!=null)
            {
             
                objFromDb.Active = model.Active;
            }
        }
    }
}
