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
                             StatusDescripcion = c.Status == true ? "Activo" : "Inactivo",
                             FechaExpiracionCuponDate=c.FechaExpiracionCupon

                         });

           

            return query.FirstOrDefault();
        }

        public CuponeraViewModel GetCuponByIdNegocio(int idNegocio)
        {
            var cupon = _db.CuponNegocios.AsQueryable();

            CuponeraViewModel cuponByBusinessId = (from c in cupon where c.IdNegocio == idNegocio && c.Status == true select new CuponeraViewModel {
                Id = c.Id,
                IdNegocio = c.IdNegocio,
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
                TipoDescuento = c.DescuentoMonto == true ? "Monetario" : "Porcentual",
                StatusDescripcion = c.Status == true ? "Activo" : "Inactivo",
                FechaExpiracionCuponDate = c.FechaExpiracionCupon,
                MontoConMonedaDescripcion = c.DescuentoMonto == true ? ((c.MonedaMonto == 1 ? "C$" : "$") + c.ValorCupon) : "%" + c.ValorCupon

            }).FirstOrDefault();

            return cuponByBusinessId;
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
                             StatusDescripcion = c.Status == true ? "Activo" : "Inactivo",
                             MontoConMonedaDescripcion= c.DescuentoMonto == true?((c.MonedaMonto == 1 ? "C$" : "$")+c.ValorCupon  ):"%"+c.ValorCupon
                         }); 

            if (!string.IsNullOrEmpty(userId))
            {
                query = query.Where(x=>x.IdUserOwner==userId);
            }

            return query.OrderByDescending(x=>x.StatusDescripcion).ToList();
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

        public bool VerifyActiveCupon(int idNegocio)
        {
            var cupon = _db.CuponNegocios.AsQueryable();
            bool ban = false;
            int count = (from c in cupon where c.IdNegocio == idNegocio && c.Status == true select c).Count();

            if (count>0)
            {
                ban = true;
            }

            return ban;
        }
    }
}
