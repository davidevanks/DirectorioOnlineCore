using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataApp.Interface;
using ModelApp.Models;
using Npgsql;

namespace DataApp.Implementation
{
    public class ManageRolRepository:IManageRolRepository
    {
        private PsqlConfiguration _conString;

        public ManageRolRepository(PsqlConfiguration conString)
        {
            _conString = conString;
        }
        public async Task<List<PlanViewModel>> GetListPlan()
        {
            var db = dbcon();
            var sql = @"
                        select * from dbo.""IdentityRole"" where showweb =true;
                        ";

            var listPlan= await db.QueryAsync<PlanViewModel>(sql, new { });
          
            return listPlan.ToList();
        }

        protected NpgsqlConnection dbcon()
        {
            return new NpgsqlConnection(_conString.ConString);
        }

    }
}
