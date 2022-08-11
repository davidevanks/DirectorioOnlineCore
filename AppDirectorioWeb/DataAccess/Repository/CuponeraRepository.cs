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
    public class CuponeraRepository : Repository<CuponNegocio>, ICuponeraRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public CuponeraRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        public CuponeraViewModel GetCuponById(int id)
        {
            var userDetails = _db.UserDetails.AsQueryable();
            var cupons = _db.CuponNegocios.AsQueryable();
            var business = _db.Negocios.AsQueryable();

            var query = (from c in cupons
                         join b in business on c.IdNegocio equals b.Id
                         join us in userDetails on b.IdUserOwner equals us.UserId
                         where c.Id==id
                         select new CuponeraViewModel
                         {
                             Id = c.Id,
                             IdNegocio = b.Id,
                             IdUsuarioCreacion = c.IdUsuarioCreacion,
                             IdUsuarioModificacion = c.IdUsuarioModificacion,
                             DescripcionPromocion = c.DescripcionPromocion,
                             DescuentoMonto = c.DescuentoMonto,
                             DescuentoPorcentaje = c.DescuentoPorcentaje,
                             CantidadCuponDisponible = c.CantidadCuponDisponible,
                             CantidadCuponUsados = c.CantidadCuponUsados,
                             FechaCreacion = c.FechaCreacion.ToShortDateString(),
                             FechaExpiracionCupon = c.FechaExpiracionCupon.ToShortDateString(),
                             FechaModificacion = c.FechaModificacion == null ? "" : c.FechaModificacion.Value.ToShortDateString(),
                             ImagenCupon = c.ImagenCupon,
                             MonedaMonto = c.MonedaMonto,
                             Status = c.Status,
                             ValorCupon = c.ValorCupon,
                             NombreNegocio = b.NombreNegocio,
                             IdUserOwner = b.IdUserOwner,
                             TipoDescuento = c.DescuentoMonto == true ? "Monetario" : "Porcentual",
                             StatusDescripcion = c.Status == true ? "Activo" : "Inactivo"

                         });

           

            return query.FirstOrDefault();
        }

        public List<CuponeraViewModel> GetCupons(string userId)
        {
            var userDetails = _db.UserDetails.AsQueryable();
            var cupons = _db.CuponNegocios.AsQueryable();
            var business = _db.Negocios.AsQueryable();

            var query = (from c in cupons
                         join b in business on c.IdNegocio equals b.Id
                         join us in userDetails on b.IdUserOwner equals us.UserId
                         select new CuponeraViewModel
                         {
                             Id = c.Id,
                             IdNegocio = b.Id,
                             IdUsuarioCreacion = c.IdUsuarioCreacion,
                             IdUsuarioModificacion = c.IdUsuarioModificacion,
                             DescripcionPromocion = c.DescripcionPromocion,
                             DescuentoMonto = c.DescuentoMonto,
                             DescuentoPorcentaje = c.DescuentoPorcentaje,
                             CantidadCuponDisponible = c.CantidadCuponDisponible,
                             CantidadCuponUsados = c.CantidadCuponUsados,
                             FechaCreacion = c.FechaCreacion.ToShortDateString(),
                             FechaExpiracionCupon = c.FechaExpiracionCupon.ToShortDateString(),
                             FechaModificacion = c.FechaModificacion == null ? "" : c.FechaModificacion.Value.ToShortDateString(),
                             ImagenCupon = c.ImagenCupon,
                             MonedaMonto = c.MonedaMonto,
                             Status = c.Status,
                             ValorCupon = c.ValorCupon,
                             NombreNegocio = b.NombreNegocio,
                             IdUserOwner = b.IdUserOwner,
                             TipoDescuento = c.DescuentoMonto == true ? "Monetario" : "Porcentual",
                             StatusDescripcion = c.Status == true ? "Activo" : "Inactivo"

                         }); 

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x=>x.IdUserOwner==userId);
            }

            return query.ToList();
        }

        public void Update(CuponNegocio cuponNegocio)
        {
            var objFromDb = _db.CuponNegocios.FirstOrDefault(s => s.Id == cuponNegocio.Id);
            if (objFromDb != null)
            {
                objFromDb.DescripcionPromocion = cuponNegocio.DescripcionPromocion;
                objFromDb.CantidadCuponDisponible = cuponNegocio.CantidadCuponDisponible;
                objFromDb.CantidadCuponUsados = cuponNegocio.CantidadCuponUsados;
                objFromDb.DescuentoMonto = cuponNegocio.DescuentoMonto;
                objFromDb.DescuentoPorcentaje = cuponNegocio.DescuentoPorcentaje;
                objFromDb.FechaExpiracionCupon = cuponNegocio.FechaExpiracionCupon;
                objFromDb.IdUsuarioModificacion = cuponNegocio.IdUsuarioModificacion;
                objFromDb.FechaModificacion = cuponNegocio.FechaModificacion;
                objFromDb.ImagenCupon = cuponNegocio.ImagenCupon;
                objFromDb.ValorCupon = cuponNegocio.ValorCupon;
                objFromDb.MonedaMonto = cuponNegocio.MonedaMonto;
                objFromDb.Status = cuponNegocio.Status;
            }
        }
    }
}
