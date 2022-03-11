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
    public class SyncData
    {
        public static async Task Task(List<PlatformModel> listPlatform)
        {
            if (listPlatform.Count < 0)
            {
                StandardMessage.ListErrorMessage();
            }
            else
            {
                using IDbConnection connection = new SqlConnection(DBConnection.ConnectionString);

                bool isDataExist = await connection.ExecuteScalarAsync<bool>("select count(1) from dbo.Platform", new { });

                if (!isDataExist)
                {
                    StandardMessage.DataNotExistMessage();
                    await InsertData.Task(listPlatform);
                }
                else
                {
                    StandardMessage.DataExistMessage();
                    await UpdateData.Task(listPlatform);
                }
            }

        }
    }
}
