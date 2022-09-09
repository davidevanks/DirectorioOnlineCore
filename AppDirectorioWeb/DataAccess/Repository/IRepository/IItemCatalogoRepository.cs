using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IItemCatalogoRepository : IRepository<ItemCatalogo>
    {
        void Update(ItemCatalogo itemCatalogo);

        List<ItemCatalogoViewModel> GetItemsCatalogo(int idConfigCatalogo);

        ItemCatalogoViewModel GetItemCatalogoById(int id);
        
       
    }
}
