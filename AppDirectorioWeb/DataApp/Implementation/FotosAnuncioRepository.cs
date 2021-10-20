using Dapper;
using DataApp.Interface;
using ModelApp.Dto.FotosAnuncio;
using ModelApp.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataApp.Implementation
{
    public class FotosAnuncioRepository : IFotosAnuncioRepository
    {
        #region Private Fields

        private PsqlConfiguration _conString;

        #endregion Private Fields

        #region Public Constructors

        public FotosAnuncioRepository(PsqlConfiguration conString)
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

        public async Task<IEnumerable<FotosAnuncioConsultarDto>> GetAllFotosAnuncio(int ID)
        {
            var db = dbcon();
            var sql = "SELECT * FROM public.\"FotosAnuncio\" where \"Activo\" = '1' and \"IdNegocio\" = " + ID;
            return await db.QueryAsync<FotosAnuncioConsultarDto>(sql, new { });
        }

        public async Task<FotosAnuncioConsultarDto> GetByIdFotosAnuncio(int ID)
        {
            var db = dbcon();
            var sql = "SELECT * FROM public.\"FotosAnuncio\" where \"Activo\" = '1' and \"Id\" = " + ID;
            return await db.QueryFirstOrDefaultAsync<FotosAnuncioConsultarDto>(sql, new { });
        }
        public async Task<int> InsertFotosAnuncio(FotosAnuncioCrearDto FotosAnuncioCrearDto)
        {
            var db = dbcon();
            var sql = "INSERT INTO public.\"FotosAnuncio\"( \"IdNegocio\", \"FechaCreacion\", \"FechaModificacion\", \"IdUsuarioModificacion\", \"IdUsuarioCreacion\", \"Foto\", \"Activo\") " +
                       "VALUES (" + FotosAnuncioCrearDto.IdNegocio + ", '" +
                                    FotosAnuncioCrearDto.FechaCreacion + "', '" +
                                    FotosAnuncioCrearDto.FechaModificacion + "', " +
                                    FotosAnuncioCrearDto.IdUsuarioCreacion + ", " +
                                    FotosAnuncioCrearDto.IdUsuarioModificacion + ", '" +
                                    FotosAnuncioCrearDto.Foto + "', '" +
                                    (FotosAnuncioCrearDto.Activo ? 1 : 0)+"');";
                       
            return await db.ExecuteAsync(sql, new { });
        }

        public async Task<int> UpdateFotosAnuncio(FotosAnuncioModificarDto FotosAnuncioCrearDto)
        {
            var db = dbcon();
            var sql = "UPDATE public.\"FotosAnuncio\" SET \"IdNegocio\"="+ FotosAnuncioCrearDto.IdNegocio + ", \"FechaCreacion\"='"+ FotosAnuncioCrearDto.FechaCreacion + "', \"FechaModificacion\"='"+ FotosAnuncioCrearDto.FechaModificacion + "', \"IdUsuarioModificacion\"="+ FotosAnuncioCrearDto.IdUsuarioModificacion+ ", \"IdUsuarioCreacion\"="+ FotosAnuncioCrearDto.IdUsuarioCreacion + ", \"Foto\"='"+ FotosAnuncioCrearDto.Foto + "', \"Activo\"= '"+ (FotosAnuncioCrearDto.Activo ? 1 : 0) + "' WHERE Id = " + FotosAnuncioCrearDto.Id;
            return await db.ExecuteAsync(sql, new { });
        }

        public async Task<int> DeleteFotosAnuncio(int ID)
        {
            var db = dbcon();
            var sql = "UPDATE public.\"FotosAnuncio\" SET \"Activo\" = '0'  where \"Id\" = " + ID;
            return await db.ExecuteAsync(sql, new { });
        }
        #endregion Public Methods


    }
}