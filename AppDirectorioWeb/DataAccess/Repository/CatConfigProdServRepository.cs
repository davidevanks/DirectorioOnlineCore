using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CatConfigProdServRepository: Repository<ConfigCatalogo>, ICatConfigProdServRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public CatConfigProdServRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }
    }
}
