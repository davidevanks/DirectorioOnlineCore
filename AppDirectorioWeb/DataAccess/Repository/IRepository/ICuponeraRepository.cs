using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICuponeraRepository : IRepository<CuponNegocio>
    {
        void Update(CuponNegocio cuponNegocio);
    }
}
