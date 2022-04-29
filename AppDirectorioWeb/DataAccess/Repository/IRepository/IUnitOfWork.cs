using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork:IDisposable
    {
        ICategoryRepository Category { get; }
        ISP_Call SP_CALL { get; }

        public IUserDetailsRepository UserDetail { get;  }

        public IDepartamentRepository Departament { get; }
        void Save();
    }
}
