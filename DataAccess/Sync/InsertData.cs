using Dapper;
using Common.Helpers;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Common;

namespace SyncDataProcess.Sync
{
    public class InsertData
    {
        public static async Task Task(List<PlatformModel> listPlatform)
        {
            using IDbConnection connection = new SqlConnection(DBConnection.ConnectionString);

            //add data process
            string sqlInsertPlatform = "insert into dbo.Platform (Id,UniqueName,Latitude,Longitude,CreatedAt,UpdatedAt)" +
                "Values (@Id,@UniqueName,@Latitude,@Longitude,@CreatedAt,@UpdatedAt)";

            string sqlInsertWell = "insert into dbo.Well (Id,UniqueName,Latitude,Longitude,CreatedAt,UpdatedAt,PlatformId)" +
                "Values (@Id,@UniqueName,@Latitude,@Longitude,@CreatedAt,@UpdatedAt,@PlatformId)";

            try
            {
                CombineModel model = MapList.AddList(listPlatform);

                int insertPlatformCommand = await connection.ExecuteAsync(sqlInsertPlatform, model.ListPlatform);

                int insertWellCommand = await connection.ExecuteAsync(sqlInsertWell, model.ListWell);

                StandardMessage.RowAffectedMessage(insertPlatformCommand, insertWellCommand, "insert");
            }
            catch (Exception ex)
            {
                StandardMessage.InsertErrorMessage(ex.Message.ToString());
            }

        }
    }
}
