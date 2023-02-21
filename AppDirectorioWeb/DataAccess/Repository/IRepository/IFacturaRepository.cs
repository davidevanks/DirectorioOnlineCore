using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IFacturaRepository : IRepository<Factura>
    {
        List<FacturaViewModel> GetInvoice(string userId);

        FacturaViewModel GetDetailInvoice(int id);

        void Update(Factura factura);
    }
}
