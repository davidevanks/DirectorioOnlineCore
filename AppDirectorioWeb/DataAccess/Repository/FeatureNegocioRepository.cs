using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FeatureNegocioRepository : Repository<FeatureNegocio>, IFeatureNegocioRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public FeatureNegocioRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        public void InsertList(List<FeatureNegocio> features)
        {
            _db.FeatureNegocios.AddRange(features);
        }

        public void Update(FeatureNegocio feature)
        {
            var objFromDb = _db.FeatureNegocios.FirstOrDefault(s => s.Id == feature.Id);
            if (objFromDb != null)
            {
                objFromDb.IdFeature = feature.IdFeature;
                objFromDb.IdNegocio = feature.IdNegocio;
                objFromDb.Activo = feature.Activo;
                objFromDb.IdUserUpdate = feature.IdUserUpdate;
                objFromDb.UpdateDate = feature.UpdateDate;
            
            }
        }
    }
}
