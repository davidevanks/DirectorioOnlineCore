using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly DirectorioOnlineCoreContext _db;

        public UnitOfWork(DirectorioOnlineCoreContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            SP_CALL = new SP_CALL(_db);
            UserDetail = new UserDetailRepository(_db);
        }

        public ICategoryRepository Category { get; private set; }
        public ISP_Call SP_CALL { get; private set; }
        public IUserDetailsRepository UserDetail { get; private set; }
     

        public void Dispose()
        {
           _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
