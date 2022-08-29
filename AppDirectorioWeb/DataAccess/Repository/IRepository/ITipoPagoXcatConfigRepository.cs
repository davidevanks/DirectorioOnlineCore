using System;
using DataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ITipoPagoXcatConfigRepository : IRepository<CatTipoPagoXcatalogoConfig>
    {
        void Update(CatTipoPagoXcatalogoConfig model);
    }
}
