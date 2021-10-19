using Dapper;
using DataApp.Interface;
using ModelApp.Models;
using Npgsql;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataApp.Implementation
{
    public class CatCatalogoRepository : ICatCatalogoRepository
    {
        #region Private Fields

        private PsqlConfiguration _conString;

        #endregion Private Fields

        #region Public Constructors

        public CatCatalogoRepository(PsqlConfiguration conString)
        {
            _conString = conString;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected NpgsqlConnection dbcon()
        {
            return new NpgsqlConnection(_conString.ConString);
        }

        #endregion Protected Methods

        #region Public Methods

        public async Task<IEnumerable<CatCatalogosViewModel>> GetAllCatalogos()
        {
            var db = dbcon();
            var sql = @"
                        select * from public.""CatCatalogos""
                        ";
            return await db.QueryAsync<CatCatalogosViewModel>(sql, new { });
        }

        public async Task<IEnumerable<CatCatalogosViewModel>> GetCatalogosxNombrePadre(string NombrePadre)
        {
            var db = dbcon();
            var sql = "select * from public.\"CatCatalogos\" where \"IdPadre\" = (select \"Id\" from public.\"CatCatalogos\" where \"Nombre\" = '"+NombrePadre+"')";
            var a =  await db.QueryAsync<CatCatalogosViewModel>(sql, new {  });
            return a;
        }

        public async Task<IEnumerable<CatCatalogosViewModel>> GetCatalogosxId(int id)
        {
            var db = dbcon();
            var sql = "select * from public.\"CatCatalogos\" where \"IdPadre\" = " + id + "";
            var a = await db.QueryAsync<CatCatalogosViewModel>(sql, new { });
            return a;
        }

        #endregion Public Methods


    }
}