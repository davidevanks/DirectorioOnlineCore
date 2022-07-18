using DataAccess.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class FacturaRepository : Repository<Factura>, IFacturaRepository
    {

        private readonly DirectorioOnlineCoreContext _db;
        public FacturaRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }
    }
}
