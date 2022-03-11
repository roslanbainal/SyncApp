using Common.Helpers;
using Common.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncDataProcess.Sync
{
    public class GetSingleData
    {
        public static async Task<PlatformModel> Platform(int id)
        {
            using IDbConnection connection = new SqlConnection(DBConnection.ConnectionString);

            string sql = "select * from dbo.Platform where Id = @Id";
            PlatformModel platform = await connection.QueryFirstOrDefaultAsync<PlatformModel>(sql, new {Id = id}) ;

            if(platform != null) return platform ;

            return null ;
        }

        public static async Task<WellModel> Well(int id)
        {
            using IDbConnection connection = new SqlConnection(DBConnection.ConnectionString);

            string sql = "select * from dbo.Well where Id = @Id";
            WellModel well = await connection.QueryFirstOrDefaultAsync<WellModel>(sql, new { Id = id });

            if (well != null) return well;

            return null;
        }

    }
}
