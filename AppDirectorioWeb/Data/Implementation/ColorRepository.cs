using Dapper;
using Data.Interface;
using Models.Dto;
using Models.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementation
{
    public class ColorRepository : IColorRepository
    {
        private PsqlConfiguration _conString;
        public ColorRepository(PsqlConfiguration conString)
        {
            _conString = conString;
        }
        protected NpgsqlConnection dbcon()
        {
            return new NpgsqlConnection(_conString.ConString);
        }

        public async Task<bool> DeleteColor(ColorsDeleteDto model)
        {
            var db = dbcon();
            var sql = @"
                        DELETE FROM color
                        WHERE color_id = @color_id;
                        ";
            var result = await db.ExecuteAsync(sql, new {  model.color_id });
            return result > 0;
        }

        public async Task<IEnumerable<Colors>> GetAllColor()
        {
            var db = dbcon();
            var sql = @"
                        SELECT * FROM color
                        ";
            return await db.QueryAsync<Colors>(sql, new { });
        }

        public async Task<Colors> GetColor(int id)
        {
            var db = dbcon();
            var sql = @"
                        SELECT * FROM color where color_id = @Id
                        ";
            return await db.QueryFirstOrDefaultAsync<Colors>(sql, new { Id = id });
        }

        public async Task<bool> InsertColor(ColorsCreateDto model)
        {
            var db = dbcon();
            var sql = @"
                        INSERT INTO color  (color_name)
                        VALUES (@color_name);
                        ";
            var result = await db.ExecuteAsync(sql, new { model.color_name });
            return result > 0;
        }

        public async Task<bool> UpdateColor(ColorsUpdateDto model)
        {
            var db = dbcon();
            var sql = @"
                        UPDATE color
                        SET color_name = @color_name
                        WHERE color_id = @color_id;
                        ";
            var result = await db.ExecuteAsync(sql, new { model.color_name, model.color_id });
            return result > 0;
        }
    }
}
