using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class logErrorRepository : Repository<LogError>, IlogErrorRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public logErrorRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }
    }
}
