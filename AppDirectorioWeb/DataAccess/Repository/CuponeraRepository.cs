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

        public void SaveCuponRedencionUsuario(int idCupon, string userId)
        {
            _db.CuponRedencionUsuarios.Add(new CuponRedencionUsuario { 
            IdCupon=idCupon,
            IdUsuario=userId,
            FechaRedencion=DateTime.Now
            });
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
                             NombrePromocion=c.NombrePromocion,
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
                             FechaExpiracionCuponDate=c.FechaExpiracionCupon,
                             MontoConMonedaDescripcion = c.DescuentoMonto == true ? ((c.MonedaMonto == 1 ? "C$" : "$") + c.ValorCupon) : "%" + c.ValorCupon,
                             CuponeaDisponibles = c.CantidadCuponDisponible - c.CantidadCuponUsados
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
                NombrePromocion = c.NombrePromocion,
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
                MontoConMonedaDescripcion = c.DescuentoMonto == true ? ((c.MonedaMonto == 1 ? "C$" : "$") + c.ValorCupon) : "%" + c.ValorCupon,
                CuponeaDisponibles=c.CantidadCuponDisponible-c.CantidadCuponUsados
                
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
                             NombrePromocion = c.NombrePromocion,
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
                             MontoConMonedaDescripcion= c.DescuentoMonto == true?((c.MonedaMonto == 1 ? "C$" : "$")+c.ValorCupon  ):"%"+c.ValorCupon,
                             CuponeaDisponibles = c.CantidadCuponDisponible - c.CantidadCuponUsados
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
                objFromDb.NombrePromocion = cuponNegocio.NombrePromocion;
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

        public void UpdateCuponesUsados(int idCupon)
        {
            var objFromDb = _db.CuponNegocios.FirstOrDefault(s => s.Id == idCupon);
            if (objFromDb != null)
            {
               
                objFromDb.CantidadCuponUsados++;
              
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

        public int ContadorCuponRedimidoXusuario(int idCupon, string userId)
        {
            int contador = _db.CuponRedencionUsuarios.Where(x=>x.IdCupon==idCupon && x.IdUsuario==userId).Count();
            return contador;
        }

        public List<CuponeraViewModel> GetCuponsActive()
        {
            var userDetails = _db.UserDetails.AsQueryable();
            var cupons = _db.CuponNegocios.AsQueryable();
            var business = _db.Negocios.AsQueryable();

            var query = (from c in cupons
                         join b in business on c.IdNegocio equals b.Id
                         join us in userDetails on b.IdUserOwner equals us.UserId
                         where c.Status==true && b.Status==17
                         select new CuponeraViewModel
                         {
                             Id = c.Id,
                             IdNegocio = b.Id,
                             IdUsuarioCreacion = c.IdUsuarioCreacion,
                             IdUsuarioModificacion = c.IdUsuarioModificacion,
                             NombrePromocion = c.NombrePromocion,
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
                             MontoConMonedaDescripcion = c.DescuentoMonto == true ? ((c.MonedaMonto == 1 ? "C$" : "$") + c.ValorCupon) : "%" + c.ValorCupon,
                             CuponeaDisponibles = c.CantidadCuponDisponible - c.CantidadCuponUsados
                         });

         

            return query.OrderByDescending(x => x.StatusDescripcion).ToList();
        }

        public void DeactivateCuponExpired()
        {
            var cuponsExpired = _db.CuponNegocios.AsQueryable();
                                                 // 26/08/2022<14/10/2022
            var query = cuponsExpired.Where(x=>x.FechaExpiracionCupon<DateTime.Now).ToList();

            foreach (var item in query)
            {
                var cupon=_db.CuponNegocios.FirstOrDefault(s => s.Id == item.Id);
                cupon.Status = false;
                _db.CuponNegocios.Update(cupon);
                _db.SaveChanges();
            }
        }
    }
}
