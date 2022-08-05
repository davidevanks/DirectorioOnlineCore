using DataAccess.Models;
using DataAccess.Repository.IRepository;
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
