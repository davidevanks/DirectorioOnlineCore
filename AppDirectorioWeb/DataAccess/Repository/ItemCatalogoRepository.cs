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
    public class ItemCatalogoRepository : Repository<ItemCatalogo>, IItemCatalogoRepository
    {
        private readonly DirectorioOnlineCoreContext _db;

        public ItemCatalogoRepository(DirectorioOnlineCoreContext db) : base(db)
        {
            _db = db;
        }
        //metodo para el businessAdmin
        public ItemCatalogoViewModel GetItemCatalogoById(int id)
        {
            var itemCatalogo = _db.ItemCatalogos.AsQueryable();
            var categorisItem = _db.CatCategoria.AsQueryable();
            var catConfig = _db.ConfigCatalogos.AsQueryable();

            var query = (from ic in itemCatalogo
                         join cat in categorisItem on ic.IdCategoriaItem equals cat.Id
                         join confCat in catConfig on ic.IdConfigCatalogo equals confCat.Id
                         where ic.Id == id
                         select new ItemCatalogoViewModel
                         {
                             Id = ic.Id,
                             IdConfigCatalogo = ic.IdConfigCatalogo,
                             IdCategoriaItem = ic.IdCategoriaItem,
                             IdUsuarioActualizacion = ic.IdUsuarioActualizacion,
                             IdUsuarioCreacion = ic.IdUsuarioCreacion,
                             NombreItem = ic.NombreItem,
                             DescripcionItem = ic.DescripcionItem,
                             Activo = ic.Activo,
                             DescripcionActivo = ic.Activo == true ? "Activo" : "Inactivo",
                             PorcentajeDescuento = ic.PorcentajeDescuento,
                             TieneDescuento = ic.TieneDescuento,
                             DescripcionTieneDescuento = ic.TieneDescuento == true ? "Sí" : "No",
                             CodigoRef = ic.NombreItem.Substring(0, 1) + ic.Id.ToString(),
                             ImagenItem = ic.ImagenItem,
                             PrecioUnitario = ic.PrecioUnitario,
                             FechaCreacion = ic.FechaCreacion,
                             FechaActualizacion = ic.FechaActualizacion,
                             NombreMoneda = confCat.IdMoneda == 1 ? "Córdobas" : "Dólares",
                             PrecioUnitarioConDescuento = (decimal)(confCat.DescuentoMasivo == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (confCat.PorcentajeDescuentoMasivo / 100))) : (ic.TieneDescuento == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (ic.PorcentajeDescuento / 100))) : 0))
                             
                         });

            return query.FirstOrDefault();
        }

        public ItemCatalogoViewModel GetItemCatalogoByIdForBueyrs(int id)
        {
            var itemCatalogo = _db.ItemCatalogos.AsQueryable();
            var categorisItem = _db.CatCategoria.AsQueryable();
            var catConfig = _db.ConfigCatalogos.AsQueryable();

            var query = (from ic in itemCatalogo
                         join cat in categorisItem on ic.IdCategoriaItem equals cat.Id
                         join confCat in catConfig on ic.IdConfigCatalogo equals confCat.Id
                         where confCat.Activo == true && ic.Activo == true && ic.Id == id
                         select new ItemCatalogoViewModel
                         {
                             Id = ic.Id,
                             IdConfigCatalogo = ic.IdConfigCatalogo,
                             IdCategoriaItem = ic.IdCategoriaItem,
                             IdUsuarioActualizacion = ic.IdUsuarioActualizacion,
                             IdUsuarioCreacion = ic.IdUsuarioCreacion,
                             NombreItem = ic.NombreItem,
                             DescripcionItem = ic.DescripcionItem,
                             Activo = ic.Activo,
                             DescripcionActivo = ic.Activo == true ? "Activo" : "Inactivo",
                             PorcentajeDescuento = ic.PorcentajeDescuento,
                             TieneDescuento = ic.TieneDescuento,
                             DescripcionTieneDescuento = ic.TieneDescuento == true ? "Sí" : "No",
                             CodigoRef = ic.NombreItem.Substring(0, 1) + ic.Id.ToString(),
                             ImagenItem = ic.ImagenItem,
                             PrecioUnitario = ic.PrecioUnitario,
                             FechaCreacion = ic.FechaCreacion,
                             FechaActualizacion = ic.FechaActualizacion,
                             NombreMoneda = confCat.IdMoneda == 1 ? "Córdobas" : "Dólares",
                             PrecioUnitarioConDescuento = (decimal)(confCat.DescuentoMasivo == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (confCat.PorcentajeDescuentoMasivo / 100))) : (ic.TieneDescuento == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (ic.PorcentajeDescuento / 100))) : 0))
                         });

            return query.FirstOrDefault();
        }

        //metodo para el businessAdmin
        public List<ItemCatalogoViewModel> GetItemsCatalogo(int idConfigCatalogo)
        {
            var itemCatalogo = _db.ItemCatalogos.AsQueryable();
            var categorisItem = _db.CatCategoria.AsQueryable();
            var catConfig = _db.ConfigCatalogos.AsQueryable();

            var query = (from ic in itemCatalogo 
                         join cat in categorisItem on ic.IdCategoriaItem equals cat.Id
                         join confCat in catConfig on ic.IdConfigCatalogo equals confCat.Id
                         where  ic.IdConfigCatalogo==idConfigCatalogo
                         select new ItemCatalogoViewModel 
                         {
                          Id=ic.Id,
                          IdConfigCatalogo=ic.IdConfigCatalogo,
                          IdCategoriaItem=ic.IdCategoriaItem,
                          IdUsuarioActualizacion=ic.IdUsuarioActualizacion,
                          IdUsuarioCreacion=ic.IdUsuarioCreacion,
                          NombreItem=ic.NombreItem,
                          DescripcionItem=ic.DescripcionItem,
                          Activo=ic.Activo,
                          DescripcionActivo= ic.Activo == true ? "Sí" : "No",
                          PorcentajeDescuento=ic.PorcentajeDescuento,
                          TieneDescuento=ic.TieneDescuento,
                          DescripcionTieneDescuento= ic.TieneDescuento == true ? "Sí" : "No",
                          CodigoRef= ic.NombreItem.Substring(0,1)+ic.Id.ToString(),
                          ImagenItem=ic.ImagenItem,
                          PrecioUnitario=ic.PrecioUnitario,
                          FechaCreacion=ic.FechaCreacion,
                          FechaActualizacion=ic.FechaActualizacion,
                             NombreMoneda = confCat.IdMoneda == 1 ? "Córdobas" : "Dólares",
                             PrecioUnitarioConDescuento = (decimal)(confCat.DescuentoMasivo == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (confCat.PorcentajeDescuentoMasivo / 100))) : (ic.TieneDescuento == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (ic.PorcentajeDescuento / 100))) : 0))
                         });

            return query.ToList();
        }

        public List<ItemCatalogoViewModel> GetItemsCatalogoForBueyrs(int idConfigCatalogo)
        {
            var itemCatalogo = _db.ItemCatalogos.AsQueryable();
            var categorisItem = _db.CatCategoria.AsQueryable();
            var catConfig = _db.ConfigCatalogos.AsQueryable();

            var query = (from ic in itemCatalogo
                         join cat in categorisItem on ic.IdCategoriaItem equals cat.Id
                         join confCat in catConfig on ic.IdConfigCatalogo equals confCat.Id
                         where confCat.Activo == true && ic.Activo == true && ic.IdConfigCatalogo == idConfigCatalogo
                         select new ItemCatalogoViewModel
                         {
                             Id = ic.Id,
                             IdConfigCatalogo = ic.IdConfigCatalogo,
                             IdCategoriaItem = ic.IdCategoriaItem,
                             IdUsuarioActualizacion = ic.IdUsuarioActualizacion,
                             IdUsuarioCreacion = ic.IdUsuarioCreacion,
                             NombreItem = ic.NombreItem,
                             DescripcionItem = ic.DescripcionItem,
                             Activo = ic.Activo,
                             DescripcionActivo = ic.Activo == true ? "Activo" : "Inactivo",
                             PorcentajeDescuento = ic.PorcentajeDescuento,
                             TieneDescuento = ic.TieneDescuento,
                             DescripcionTieneDescuento = ic.TieneDescuento == true ? "Sí" : "No",
                             CodigoRef = ic.NombreItem.Substring(0, 1) + ic.Id.ToString(),
                             ImagenItem = ic.ImagenItem,
                             PrecioUnitario = ic.PrecioUnitario,
                             FechaCreacion = ic.FechaCreacion,
                             FechaActualizacion = ic.FechaActualizacion,
                             NombreMoneda = confCat.IdMoneda == 1 ? "Córdobas" : "Dólares",
                             PrecioUnitarioConDescuento = (decimal)(confCat.DescuentoMasivo == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (confCat.PorcentajeDescuentoMasivo / 100))) : (ic.TieneDescuento == true ? (ic.PrecioUnitario - (ic.PrecioUnitario * (ic.PorcentajeDescuento / 100))) : 0))
                         }); 

            return query.ToList();
        }

        public void Update(ItemCatalogo itemCatalogo)
        {
            var objFromDb = _db.ItemCatalogos.FirstOrDefault(s => s.Id == itemCatalogo.Id);
            if (objFromDb != null)
            {
                objFromDb.IdConfigCatalogo = itemCatalogo.IdConfigCatalogo;
                objFromDb.IdCategoriaItem = itemCatalogo.IdCategoriaItem;
                objFromDb.NombreItem = itemCatalogo.NombreItem;
                objFromDb.DescripcionItem = itemCatalogo.DescripcionItem;
                objFromDb.PrecioUnitario = itemCatalogo.PrecioUnitario;
                objFromDb.TieneDescuento = itemCatalogo.TieneDescuento;
                objFromDb.PorcentajeDescuento = itemCatalogo.PorcentajeDescuento;
                objFromDb.ImagenItem = itemCatalogo.ImagenItem;
                objFromDb.Activo = itemCatalogo.Activo;
                objFromDb.FechaActualizacion = DateTime.Now;
                objFromDb.IdUsuarioActualizacion = itemCatalogo.IdUsuarioActualizacion;

            }
        }
    }
}
