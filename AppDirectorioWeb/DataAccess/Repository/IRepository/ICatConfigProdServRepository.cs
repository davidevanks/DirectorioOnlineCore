using DataAccess.Models;
using Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface ICatConfigProdServRepository : IRepository<ConfigCatalogo>
    {
        List<ConfigCatalogoViewModel> lstConfigCat(int? idNegocio);

        ConfigCatalogoViewModel GetConfigCatById(int? id);

        void UpdateCatConfig(ConfigCatalogo configCatalogo);
        bool VerifyActiveCatConfig(int idNegocio);
        List<CatTipoPagoXcatalogoConfigViewModel> GetLstTipoPagoByIdCatConfig(int? id);

        string GetStringNamesTipoPago(int idCatConfig);
    }
}
