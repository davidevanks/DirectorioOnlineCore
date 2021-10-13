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
            var sql = @"
                        SELECT * FROM public.""AnuncioInfo""
                        ";
            return await db.QueryAsync<AnuncioInfoConsultarDto>(sql, new { });
        }

        public async Task<AnuncioInfoConsultarDto> GetByIdAnuncioInfo(int ID)
        {
            var db = dbcon();
            var sql = @"
                        SELECT * FROM public.""AnuncioInfo"" where color_id = @Id
                        ";
            return await db.QueryFirstOrDefaultAsync<AnuncioInfoConsultarDto>(sql, new { Id = ID });
        }
        public async Task<int> InsertAnuncioInfo(AnuncioInfoCrearDto AnuncioInfoCrearDto)
        {
            var db = dbcon();
            var sql = @"
                        INSERT INTO PUBLIC.""AnuncioInfo""
                            (""NombreNegocio"", ""IdCategoria"", ""DescripcionNegocio"", ""IdPais"", ""IdDepartamento"", ""DireccionNegocio"", ""Longitud"", ""Latitud"", ""TelefonoNegocio"", ""EmailNegocio"", ""PaginaWebNegocio"", ""InstagramNegocio"", ""FacebookNegocio"", ""TwitterNegocio"", ""WhatsApp"", ""TieneDelivery"", ""Hugo"", ""PedidosYa"", ""Piki"", ""FechaCreacion"", ""FechaModificacion"", ""IdUsuarioCreacion"", ""IdUsuarioModificacion"", ""Id"", ""HabilitarHorarioFeriado"", ""Calificacion"", ""LogoNegocio"", ""Idusuario"", ""Activo"")
                VALUES      (@nombrenegocio,
                             @idcategoria,
                             @descripcionnegocio,
                             @idpais,
                             @iddepartamento,
                             @direccionnegocio,
                             @longitud,
                             @latitud,
                             @telefononegocio,
                             @emailnegocio,
                             @paginawebnegocio,
                             @instagramnegocio,
                             @facebooknegocio,
                             @twitternegocio,
                             @whatsapp,
                             @tienedelivery,
                             @hugo,
                             @pedidosya,
                             @piki,
                             @fechacreacion,
                             @fechamodificacion,
                             @idusuariocreacion,
                             @idusuariomodificacion,
                             @id,
                             @habilitarhorarioferiado,
                             @calificacion,
                             @logonegocio,
                             @idusuario,
                             @activo);
                        ";
            return await db.ExecuteAsync(sql, new
            {
                nombrenegocio = AnuncioInfoCrearDto.NombreNegocio,
                idcategoria = AnuncioInfoCrearDto.IdCategoria,
                descripcionnegocio = AnuncioInfoCrearDto.DescripcionNegocio,
                idpais = AnuncioInfoCrearDto.IdPais,
                iddepartamento = AnuncioInfoCrearDto.IdDepartamento,
                direccionnegocio = AnuncioInfoCrearDto.DireccionNegocio,
                longitud = AnuncioInfoCrearDto.Longitud,
                latitud = AnuncioInfoCrearDto.Latitud,
                telefononegocio = AnuncioInfoCrearDto.TelefonoNegocio,
                emailnegocio = AnuncioInfoCrearDto.EmailNegocio,
                paginawebnegocio = AnuncioInfoCrearDto.PaginaWebNegocio,
                instagramnegocio = AnuncioInfoCrearDto.InstagramNegocio,
                facebooknegocio = AnuncioInfoCrearDto.FacebookNegocio,
                twitternegocio = AnuncioInfoCrearDto.TwitterNegocio,
                whatsapp = AnuncioInfoCrearDto.WhatsApp,
                tienedelivery = AnuncioInfoCrearDto.TieneDelivery,
                hugo = AnuncioInfoCrearDto.Hugo,
                pedidosya = AnuncioInfoCrearDto.PedidosYa,
                piki = AnuncioInfoCrearDto.Piki,
                fechacreacion = AnuncioInfoCrearDto.FechaCreacion,
                fechamodificacion = AnuncioInfoCrearDto.FechaModificacion,
                idusuariocreacion = AnuncioInfoCrearDto.IdUsuarioCreacion,
                idusuariomodificacion = AnuncioInfoCrearDto.IdUsuarioModificacion,
                // id = AnuncioInfoCrearDto.,
                habilitarhorarioferiado = AnuncioInfoCrearDto.HabilitarHorarioFeriado,
                calificacion = AnuncioInfoCrearDto.Calificacion,
                logonegocio = AnuncioInfoCrearDto.LogoNegocio,
                idusuario = AnuncioInfoCrearDto.Idusuario,
                activo = AnuncioInfoCrearDto.Activo
            });
        }

        public async Task<int> UpdateAnuncioInfo(AnuncioInfoModificarDto AnuncioInfoCrearDto)
        {
            var db = dbcon();
            var sql = @"
                        UPDATE public.""AnuncioInfo""
	                                SET ""NombreNegocio""=@NombreNegocio, 
	                                    ""IdCategoria""=@IdCategoria, 
	                                    ""DescripcionNegocio""=@DescripcionNegocio, 
	                                    ""IdPais""=@IdPais, 
	                                    ""IdDepartamento""=@IdDepartamento,
	                                    ""DireccionNegocio""=@DireccionNegocio, 
	                                    ""Longitud""=@Longitud, 
	                                    ""Latitud""=@Latitud, 
	                                    ""TelefonoNegocio""=@TelefonoNegocio, 
	                                    ""EmailNegocio""=@EmailNegocio, 
	                                    ""PaginaWebNegocio""=@PaginaWebNegocio, 
	                                    ""InstagramNegocio""=@FacebookNegocio, 
	                                    ""FacebookNegocio""=@FacebookNegocio, 
	                                    ""TwitterNegocio""=@TwitterNegocio, 
	                                    ""WhatsApp""=@WhatsApp, 
	                                    ""TieneDelivery""=@TieneDelivery,
	                                    ""Hugo""=@Hugo, 
	                                    ""PedidosYa""=@PedidosYa, 
	                                    ""Piki""=@Piki, 
	                                    ""FechaCreacion""=@FechaCreacion, 
	                                    ""FechaModificacion""=@FechaModificacion, 
	                                    ""IdUsuarioCreacion""=@IdUsuarioCreacion, 
	                                    ""IdUsuarioModificacion""=@IdUsuarioModificacion, 
	
	                                    ""HabilitarHorarioFeriado""=@HabilitarHorarioFeriado, 
	                                    ""Calificacion""=@Calificacion, 
	                                    ""LogoNegocio""=@LogoNegocio, 
	                                    ""Idusuario""=@Idusuario, 
	                                    ""Activo""=@Activo
	                                WHERE <condition>;
                        ";
            return await db.ExecuteAsync(sql, new
            {
                NombreNegocio = AnuncioInfoCrearDto.NombreNegocio,
                IdCategoria = AnuncioInfoCrearDto.IdCategoria,
                DescripcionNegocio = AnuncioInfoCrearDto.DescripcionNegocio,
                IdPais = AnuncioInfoCrearDto.IdPais,
                IdDepartamento = AnuncioInfoCrearDto.IdDepartamento,
                DireccionNegocio = AnuncioInfoCrearDto.DireccionNegocio,
                Longitud = AnuncioInfoCrearDto.Longitud,
                Latitud = AnuncioInfoCrearDto.Latitud,
                TelefonoNegocio = AnuncioInfoCrearDto.TelefonoNegocio,
                emailnegocio = AnuncioInfoCrearDto.EmailNegocio,
                PaginaWebNegocio = AnuncioInfoCrearDto.PaginaWebNegocio,
                InstagramNegocio = AnuncioInfoCrearDto.InstagramNegocio,
                FacebookNegocio = AnuncioInfoCrearDto.FacebookNegocio,
                TwitterNegocio = AnuncioInfoCrearDto.TwitterNegocio,
                WhatsApp = AnuncioInfoCrearDto.WhatsApp,
                TieneDelivery = AnuncioInfoCrearDto.TieneDelivery,
                Hugo = AnuncioInfoCrearDto.Hugo,
                PedidosYa = AnuncioInfoCrearDto.PedidosYa,
                Piki = AnuncioInfoCrearDto.Piki,
                FechaCreacion = AnuncioInfoCrearDto.FechaCreacion,
                FechaModificacion = AnuncioInfoCrearDto.FechaModificacion,
                IdUsuarioCreacion = AnuncioInfoCrearDto.IdUsuarioCreacion,
                IdUsuarioModificacion = AnuncioInfoCrearDto.IdUsuarioModificacion,
                // id = AnuncioInfoCrearDto.,
                HabilitarHorarioFeriado = AnuncioInfoCrearDto.HabilitarHorarioFeriado,
                Calificacion = AnuncioInfoCrearDto.Calificacion,
                LogoNegocio = AnuncioInfoCrearDto.LogoNegocio,
                Idusuario = AnuncioInfoCrearDto.Idusuario,
                Activo = AnuncioInfoCrearDto.Activo
            });
        }

        public async Task<int> DeleteAnuncioInfo(int ID)
        {
            var db = dbcon();
            var sql = @"
                        UPDATE public.""AnuncioInfo"" SET ""Activo"" = '0'  where color_id = @Id
                        ";
            return await db.ExecuteAsync(sql, new { Id = ID });
        }
        #endregion Public Methods


    }
}