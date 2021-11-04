using Dapper;
using DataApp.Interface;
using ModelApp.Dto.AnuncioInfo;
using ModelApp.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataApp.Implementation
{
    public class AnuncioInfoRepository : IAnuncioInfoRepository
    {
        #region Private Fields

        private PsqlConfiguration _conString;

        #endregion Private Fields

        #region Public Constructors

        public AnuncioInfoRepository(PsqlConfiguration conString)
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

        public async Task<IEnumerable<AnuncioInfoConsultarDto>> GetAllAnuncioInfo()
        {
            var db = dbcon();
            var sql = "SELECT * FROM public.\"AnuncioInfo\" where \"Activo\" = '1'";
            return await db.QueryAsync<AnuncioInfoConsultarDto>(sql, new { });
        }

        public async Task<AnuncioInfoConsultarDto> GetByIdAnuncioInfo(AnuncioInfoModificarDto model)
        {
            var db = dbcon();
            var sql = "SELECT * FROM public.\"AnuncioInfo\" where \"Activo\" = '1' and \"Id\" = " + model.Id;
            return await db.QueryFirstOrDefaultAsync<AnuncioInfoConsultarDto>(sql, new { });
        }
        public async Task<int> InsertAnuncioInfo(AnuncioInfoCrearDto AnuncioInfoCrearDto)
        {
            var db = dbcon();
            var sql = "INSERT INTO PUBLIC.\"AnuncioInfo\" " +
                           "(\"NombreNegocio\", \"IdCategoria\", \"DescripcionNegocio\", \"IdPais\", \"IdDepartamento\", \"DireccionNegocio\", \"Longitud\", \"Latitud\", \"TelefonoNegocio\", \"EmailNegocio\", \"PaginaWebNegocio\", \"InstagramNegocio\", \"FacebookNegocio\", \"TwitterNegocio\", \"WhatsApp\", \"TieneDelivery\", \"Hugo\", \"PedidosYa\", \"Piki\", \"FechaCreacion\", \"FechaModificacion\", \"IdUsuarioCreacion\", \"IdUsuarioModificacion\", \"HabilitarHorarioFeriado\", \"Calificacion\", \"LogoNegocio\", \"Idusuario\", \"Activo\")" +
                           "VALUES      ('"
                           + AnuncioInfoCrearDto.NombreNegocio + "', "
                           + AnuncioInfoCrearDto.IdCategoria + ", "
                           +"'" + AnuncioInfoCrearDto.DescripcionNegocio + "', " 
                           + AnuncioInfoCrearDto.IdPais + ", " 
                           + AnuncioInfoCrearDto.IdDepartamento + ",  '"
                           + AnuncioInfoCrearDto.DireccionNegocio + "', "
                           + AnuncioInfoCrearDto.Longitud + ", "
                           + AnuncioInfoCrearDto.Latitud + ", '"
                           + AnuncioInfoCrearDto.TelefonoNegocio + "', '" 
                           + AnuncioInfoCrearDto.EmailNegocio + "', '" 
                           + AnuncioInfoCrearDto.PaginaWebNegocio + "', '" 
                           + AnuncioInfoCrearDto.InstagramNegocio + "', '" 
                           + AnuncioInfoCrearDto.FacebookNegocio + "', '" 
                           + AnuncioInfoCrearDto.TwitterNegocio + "', '" 
                           + AnuncioInfoCrearDto.WhatsApp + "', '" 
                           + (AnuncioInfoCrearDto.TieneDelivery ? 1 : 0) + "','" 
                           + (AnuncioInfoCrearDto.Hugo ? 1 : 0) + "', '" 
                           + (AnuncioInfoCrearDto.PedidosYa ? 1 : 0) + "', '" 
                           + (AnuncioInfoCrearDto.Piki ? 1 : 0) + "','" 
                           + AnuncioInfoCrearDto.FechaCreacion + "', '" 
                           + AnuncioInfoCrearDto.FechaModificacion + "', " 
                           + AnuncioInfoCrearDto.IdUsuarioCreacion + "," 
                           + AnuncioInfoCrearDto.IdUsuarioModificacion + ",'" 
                           + (AnuncioInfoCrearDto.HabilitarHorarioFeriado ? 1 : 0) + "' ,  " 
                           + AnuncioInfoCrearDto.Calificacion + ",  '" 
                           + AnuncioInfoCrearDto.LogoNegocio + "'," 
                           + AnuncioInfoCrearDto.Idusuario + ", '" 
                           + AnuncioInfoCrearDto.Activo + "');"
                           + "update public.\"AnuncioInfo\" set geog = cast( (select 'SRID=4326;POINT(' || CAST (\"Longitud\"AS varchar(20) )|| ' ' || cast(\"Latitud\" AS varchar(20) ) || ')' from  public.\"AnuncioInfo\" where \"Id\" = (select max(\"Id\") from public.\"AnuncioInfo\")) as geography) where \"Id\" = (select max(\"Id\") from public.\"AnuncioInfo\");"
                           + " select max(\"Id\") from public.\"AnuncioInfo\"";
            return await db.QueryFirstOrDefaultAsync<int>(sql, new { });
        }

        public async Task<int> UpdateAnuncioInfo(AnuncioInfoModificarDto AnuncioInfoCrearDto)
        {
            var db = dbcon();
            var sql = "UPDATE public.\"AnuncioInfo\" SET " +
              "    \"IdCategoria\"=" + AnuncioInfoCrearDto.IdCategoria + ", " +
              "    \"NombreNegocio\"= '" + AnuncioInfoCrearDto.NombreNegocio + "', " +
              "    \"DescripcionNegocio\"= '" + AnuncioInfoCrearDto.DescripcionNegocio + "', " +
              "    \"IdPais\"=" + AnuncioInfoCrearDto.IdPais + ", " +
              "    \"IdDepartamento\"=" + AnuncioInfoCrearDto.IdDepartamento + ", " +
              "    \"DireccionNegocio\"='" + AnuncioInfoCrearDto.DireccionNegocio + "', " +
              "    \"Longitud\"=" + AnuncioInfoCrearDto.Longitud + ", " +
              "    \"Latitud\"=" + AnuncioInfoCrearDto.Latitud + ", " +
              "    \"TelefonoNegocio\"='" + AnuncioInfoCrearDto.TelefonoNegocio + "', " +
              "    \"EmailNegocio\"='" + AnuncioInfoCrearDto.EmailNegocio + "', " +
              "    \"PaginaWebNegocio\"='" + AnuncioInfoCrearDto.PaginaWebNegocio + "', " +
              "    \"InstagramNegocio\"='" + AnuncioInfoCrearDto.InstagramNegocio + "', " +
              "    \"FacebookNegocio\"='" + AnuncioInfoCrearDto.FacebookNegocio + "', " +
              "    \"TwitterNegocio\"='" + AnuncioInfoCrearDto.TwitterNegocio + "', " +
              "    \"WhatsApp\"='" + AnuncioInfoCrearDto.WhatsApp + "', " +
              "    \"TieneDelivery\"='" + (AnuncioInfoCrearDto.TieneDelivery ? 1 : 0) + "', " +
              "    \"Hugo\"='" + (AnuncioInfoCrearDto.Hugo ? 1 : 0) + "', " +
              "    \"PedidosYa\"='" + (AnuncioInfoCrearDto.PedidosYa ? 1 : 0) + "', " +
              "    \"Piki\"='" + (AnuncioInfoCrearDto.Piki ? 1 : 0) + "', " +
              "    \"FechaCreacion\"='" + AnuncioInfoCrearDto.FechaCreacion + "', " +
              "    \"FechaModificacion\"='" + AnuncioInfoCrearDto.FechaModificacion + "', " +
              "    \"IdUsuarioCreacion\"=" + AnuncioInfoCrearDto.IdUsuarioCreacion + ", " +
              "    \"IdUsuarioModificacion\"=" + AnuncioInfoCrearDto.IdUsuarioModificacion + ", " +
              "    \"HabilitarHorarioFeriado\"='" + (AnuncioInfoCrearDto.HabilitarHorarioFeriado ? 1 : 0) + "', " +
              "    \"Calificacion\"=" + AnuncioInfoCrearDto.Calificacion + ", " +
              "    \"LogoNegocio\"='" + AnuncioInfoCrearDto.LogoNegocio + "', " +
              "    \"Idusuario\"=" + AnuncioInfoCrearDto.Idusuario + ", " +
              "    \"Activo\"='" + AnuncioInfoCrearDto.Activo + "' "
              + "WHERE \"Id\" = " + AnuncioInfoCrearDto.Id;
            return await db.ExecuteAsync(sql, new { });
        }

        public async Task<int> DeleteAnuncioInfo(int ID)
        {
            var db = dbcon();
            var sql = "UPDATE public.\"AnuncioInfo\" SET \"Activo\" = '0'  where \"Id\" = " + ID;
            return await db.ExecuteAsync(sql, new { });
        }

        public async Task<IEnumerable<AnuncioInfoConsultarDto>> GetAllAnuncioBySearch(SearchBussinesRequest search)
        {
            var db = dbcon();
            var sql = "SELECT * FROM public.\"AnuncioInfo\"  WHERE ST_DWithin(geog, ST_MakePoint("+search.Longitud.ToString()+","+search.Latitud.ToString()+ "), 10000) and lower(\"NombreNegocio\") like '%"+search.Search.ToLower()+ "%' ORDER BY geog <-> ST_MakePoint(" + search.Longitud.ToString() + "," + search.Latitud.ToString() + ")";
            return await db.QueryAsync<AnuncioInfoConsultarDto>(sql, new { });
        }

        #endregion Public Methods


    }
}