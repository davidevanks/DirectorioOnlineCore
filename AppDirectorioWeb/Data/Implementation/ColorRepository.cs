using Dapper;
using Data.Interface;
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

        public Task<bool> DeleteColor()
        {
            throw new NotImplementedException();
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

        public Task<bool> InsertColor()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateColor()
        {
            throw new NotImplementedException();
        }
    }
}
