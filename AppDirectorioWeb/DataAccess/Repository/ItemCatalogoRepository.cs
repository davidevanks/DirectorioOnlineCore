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
        public ItemCatalogoViewModel GetItemCatalogoById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ItemCatalogoViewModel> GetItemsCatalogo(int idConfigCatalogo)
        {
            var itemCatalogo = _db.ItemCatalogos.AsQueryable();
            var categorisItem = _db.CatCategoria.AsQueryable();

            var query = (from ic in itemCatalogo 
                         join cat in categorisItem on ic.IdCategoriaItem equals cat.Id
                         where ic.IdConfigCatalogo==idConfigCatalogo
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
                          DescripcionActivo=


                         
                         });

            throw new NotImplementedException();
        }

        public void Update(ItemCatalogo itemCatalogo)
        {
            throw new NotImplementedException();
        }
    }
}
