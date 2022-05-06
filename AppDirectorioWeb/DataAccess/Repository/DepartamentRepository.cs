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
    public class DepartamentRepository : Repository<CatDepartamento>, IDepartamentRepository
    {
        private readonly DirectorioOnlineCoreContext _db;

        public DepartamentRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }
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
    }
}
