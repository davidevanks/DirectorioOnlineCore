using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Models.ViewModels;

namespace DataAccess.Repository
{
    public class CategoryRepository: Repository<CatCategorium>,ICategoryRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public CategoryRepository(DirectorioOnlineCoreContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<CatCategorium> GetCatUsedByBusiness()
        {
            var cat = _db.CatCategoria.AsQueryable();
            var bus = _db.Negocios.AsQueryable();

            var query=(from c  in cat join b in bus on c.Id equals b.IdCategoria
                       where b.Status==17
                       select new CatCategorium 
                       {
                            Id= c.Id,
                            IdPadre= c.IdPadre,
                            Nombre =c.Nombre,
                            Activo =c.Activo,
                            IdUserCreate= c.IdUserCreate,
                            CreateDate =c.CreateDate,
                            IdUserUpdate= c.IdUserUpdate,
                            UpdateDate =c.UpdateDate
                         }).ToList();

            return query;
        }

        public void Update(CatCategorium  category)
        {
            var objFromDb = _db.CatCategoria.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb!=null)
            {
                objFromDb.Nombre = category.Nombre;
                objFromDb.Activo = category.Activo;
                objFromDb.IdUserUpdate= category.IdUserUpdate;
                objFromDb.UpdateDate = category.UpdateDate;
            }
            
          
        }
    }
}
