using DataAccess.Models;
using DataAccess.Repository.IRepository;
using Models.ViewModels;
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

        public List<FacturaViewModel> GetInvoice(string userId)
        {
            var userDetail = _db.UserDetails.AsQueryable();
            var factura = _db.Facturas.AsQueryable();
            var plan = _db.CatPlans.AsQueryable();
            var user = _db.Users.AsQueryable();

            var query = (
                from f in factura
                join u in user on f.UserId equals u.Id
                join ud in userDetail on u.Id equals ud.UserId
                join p in plan on f.IdPlan equals p.Id
                select new FacturaViewModel
                {
                    UserId=u.Id,
                    User=u.UserName,
                    IdFactura=f.Id,
                    NoAutorizacion=f.NoAutorizacionPago,
                    FacturaPagada=f.FacturaPagada,
                    FacturaEnviada=f.FacturaEnviada,
                    FechaPago=f.FechaPago,
                    IdPlan=p.Id,
                    PlanSuscripcion=p.PlanName

                });

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x => x.UserId == userId);
            }

            return query.ToList();
        }
    }
}
