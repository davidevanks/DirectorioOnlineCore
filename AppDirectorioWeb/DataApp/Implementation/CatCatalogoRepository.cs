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

        #region Public Methods

        public async Task<IEnumerable<CatCatalogosViewModel>> GetAllCatalogos()
        {
            var db = dbcon();
            var sql = @"
                        select * from public.""CatCatalogos""
                        ";
            return await db.QueryAsync<CatCatalogosViewModel>(sql, new { });
        }

        #endregion Public Methods

        #region Protected Methods

        protected NpgsqlConnection dbcon()
        {
            return new NpgsqlConnection(_conString.ConString);
        }

        #endregion Protected Methods
    }
}