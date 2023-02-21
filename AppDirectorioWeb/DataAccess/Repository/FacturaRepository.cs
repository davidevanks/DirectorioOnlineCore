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

        public FacturaViewModel GetDetailInvoice(int id)
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
                where f.Id==id
                select new FacturaViewModel
                {
                    UserId = u.Id,
                    User = u.UserName,
                    IdFactura = f.Id,
                    NoAutorizacion = f.NoAutorizacionPago,
                    FacturaPagada = f.FacturaPagada,
                    FacturaEnviada = f.FacturaEnviada,
                    FechaPago = f.FechaPago.Value.ToShortDateString(),
                    IdPlan = p.Id,
                    PlanSuscripcion = p.PlanName,
                    UserEmail=u.Email,
                    MontoPago=f.MontoPagado.ToString()

                });

          
            return query.FirstOrDefault();
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
                    FechaPago=f.FechaPago.Value.ToShortDateString(),
                    IdPlan=p.Id,
                    PlanSuscripcion=p.PlanName

                });

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x => x.UserId == userId);
            }

            return query.ToList();
        }
        public void Update(Factura factura)
        {
            var objFromDb = _db.Facturas.FirstOrDefault(s => s.Id == factura.Id);
            if (objFromDb != null)
            {
                objFromDb.FechaActualizacion = DateTime.Now;
                objFromDb.IdUserUpdate = factura.IdUserUpdate;
                objFromDb.FechaPago = factura.FechaPago;
                objFromDb.FacturaEnviada = factura.FacturaEnviada;
                objFromDb.FacturaPagada = factura.FacturaPagada;
                objFromDb.NoAutorizacionPago = factura.NoAutorizacionPago;
            }
        }
    }
}
