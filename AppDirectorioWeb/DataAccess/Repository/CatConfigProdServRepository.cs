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
    public class CatConfigProdServRepository: Repository<ConfigCatalogo>, ICatConfigProdServRepository
    {
        private readonly DirectorioOnlineCoreContext _db;
        public CatConfigProdServRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }

        public ConfigCatalogoViewModel GetConfigCatById(int? id)
        {
            var catConfig = _db.ConfigCatalogos.AsQueryable();
            var business = _db.Negocios.AsQueryable();
            var catTipoPagoXconfigCat = _db.CatTipoPagoXcatalogoConfigs.AsQueryable();
            var catCategories = _db.CatCategoria.AsQueryable();

            var query = (from cc in catConfig
                         join b in business on cc.IdNegocio equals b.Id
                         where cc.Id==id
                         select new ConfigCatalogoViewModel
                         {
                             Id=cc.Id,
                             NombreCatalogo=cc.NombreCatalogo,
                             NombreMoneda = cc.IdMoneda == 1 ? "Córdobas" : "Dólares",
                             IdMoneda=cc.IdMoneda,
                             NombreNegocio=b.NombreNegocio,
                             NombreTipoCatalogo= cc.IdTipoCatalogo == 1 ? "Productos" : "Servicios",
                             IdTipoCatalogo=cc.IdTipoCatalogo,
                             DescuentoMasivo=cc.DescuentoMasivo,
                             PorcentajeDescuentoMasivo=cc.PorcentajeDescuentoMasivo,
                             Activo=cc.Activo,
                             DescripcionActivo = cc.Activo == true ? "Activo" : "Inactivo",
                             FechaCreacion=cc.FechaCreacion,
                             IdUsuarioCreacion=cc.IdUsuarioCreacion,
                             FechaActualizacion=cc.FechaActualizacion,
                             IdUsuarioActualizacion=cc.IdUsuarioActualizacion


                         });

            return query.FirstOrDefault();
        }

        public List<CatTipoPagoXcatalogoConfigViewModel> GetLstTipoPagoByIdCatConfig(int? id)
        {
            var catTipoPagoConfigCat = _db.CatTipoPagoXcatalogoConfigs.AsQueryable();
            var catCategories = _db.CatCategoria.AsQueryable();
            IQueryable<CatTipoPagoXcatalogoConfigViewModel> query;

            if (id!=null)
            {
                query = (from ctc in catTipoPagoConfigCat
                         join cat in catCategories on ctc.IdTipoPago equals cat.Id
                         where ctc.IdCatConfigProdServ == id
                         select new CatTipoPagoXcatalogoConfigViewModel
                         {
                             Id = ctc.Id,
                             IdCatConfigProdServ = ctc.IdCatConfigProdServ,
                             IdTipoPago = ctc.IdTipoPago,
                             NombreTipoPago = cat.Nombre,
                             Active = ctc.Active

                         });
            }
            else
            {
                query = (from cat in catCategories 
                         where cat.IdPadre == 40
                         select new CatTipoPagoXcatalogoConfigViewModel
                         {
                             Id = 0,
                             IdCatConfigProdServ = 0,
                             IdTipoPago = cat.Id,
                             NombreTipoPago = cat.Nombre,
                             Active = true

                         });
            }
                  

            return query.ToList();
        }

        public string GetStringNamesTipoPago(int idCatConfig)
        {
            var catTipoPagoXconfigCat = _db.CatTipoPagoXcatalogoConfigs.AsQueryable();
            var catCategories = _db.CatCategoria.AsQueryable();

            var query = (from tp in catTipoPagoXconfigCat
                         join cat in catCategories on tp.IdTipoPago equals cat.Id
                         where tp.IdCatConfigProdServ==idCatConfig
                         select new { 
                           TipoPagoName=cat.Nombre
                         
                         }).ToList();

           
            string names = string.Join(", ", query);

            return names;
        }

        public List<ConfigCatalogoViewModel> lstConfigCat(int? idNegocio)
        {

            var catConfig = _db.ConfigCatalogos.AsQueryable();
            var business = _db.Negocios.AsQueryable();
          

            var query = (from cc in catConfig
                         join b in business on cc.IdNegocio equals b.Id
                         select new ConfigCatalogoViewModel
                         {
                             Id = cc.Id,
                             NombreCatalogo = cc.NombreCatalogo,
                             NombreMoneda = cc.IdMoneda == 1 ? "Córdobas" : "Dólares",
                             IdMoneda = cc.IdMoneda,
                             NombreNegocio = b.NombreNegocio,
                             IdNegocio=b.Id,
                             NombreTipoCatalogo = cc.IdTipoCatalogo == 1 ? "Productos" : "Servicios",
                             IdTipoCatalogo = cc.IdTipoCatalogo,
                             DescuentoMasivo = cc.DescuentoMasivo,
                             NombreDescuentoMasivo = cc.DescuentoMasivo == true ? "Si" : "No",
                             PorcentajeDescuentoMasivo = cc.PorcentajeDescuentoMasivo,
                             Activo = cc.Activo,
                             DescripcionActivo = cc.Activo == true ? "Activo" : "Inactivo",
                             FechaCreacion = cc.FechaCreacion,
                             IdUsuarioCreacion = cc.IdUsuarioCreacion,
                             FechaActualizacion = cc.FechaActualizacion,
                             IdUsuarioActualizacion = cc.IdUsuarioActualizacion

                         });

            if (idNegocio!=null)
            {
                query = query.Where(x=>x.IdNegocio==idNegocio);
            }

            return query.ToList();
        }

        public void UpdateCatConfig(ConfigCatalogo configCatalogo)
        {
            var objFromDb = _db.ConfigCatalogos.FirstOrDefault(x=>x.Id==configCatalogo.Id);
            if (objFromDb!=null)
            {
              
                objFromDb.IdMoneda = configCatalogo.IdMoneda;
                objFromDb.IdNegocio = configCatalogo.IdNegocio;
                objFromDb.IdTipoCatalogo = configCatalogo.IdTipoCatalogo;
                objFromDb.IdUsuarioActualizacion = configCatalogo.IdUsuarioCreacion;
                objFromDb.FechaActualizacion = DateTime.Now;
                objFromDb.NombreCatalogo = configCatalogo.NombreCatalogo;
                objFromDb.DescuentoMasivo = configCatalogo.DescuentoMasivo;
                objFromDb.PorcentajeDescuentoMasivo = configCatalogo.PorcentajeDescuentoMasivo;
                objFromDb.Activo = configCatalogo.Activo;
            }
           
        }

        public bool VerifyActiveCatConfig(int idNegocio)
        {
            //me falta
            throw new NotImplementedException();
        }
    }
}
