using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
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

        public List<FeatureNegocioViewModel> GetListFeaturesByBusinessId(int id)
        {
            var features = _db.CatCategoria.AsQueryable();
            var businessFeatures = _db.FeatureNegocios.AsQueryable();

            var query = (from bf in businessFeatures join f in features on bf.IdFeature equals f.Id
                         where bf.IdNegocio==id && bf.Activo==true && f.Activo==true
                         select new FeatureNegocioViewModel { 
                         IdFeature=bf.IdFeature,
                         IdNegocio=bf.IdNegocio,
                         Id=bf.Id,
                         Feature=f.Nombre,
                         Activo=(bool)bf.Activo
                         }).ToList();

            return query;
           
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
