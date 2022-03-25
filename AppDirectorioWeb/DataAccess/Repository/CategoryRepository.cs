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

        public void Update(CatCategoryViewModel  category)
        {
            var objFromDb = _db.CatCategoria.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb!=null)
            {
                objFromDb.Nombre = category.Nombre;
                _db.SaveChanges();
            }
            
          
        }
    }
}
