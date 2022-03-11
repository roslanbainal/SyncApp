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
    public class UpdateData
    {
        public static async Task Task(List<PlatformModel> listPlatform)
        {
            using IDbConnection connection = new SqlConnection(DBConnection.ConnectionString);

            string sqlUpdatePlatform = "update dbo.Platform set UniqueName =@UniqueName, Latitude = @Latitude, Longitude = @Longitude," +
                "CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt where Id = @Id";

            string sqlUpdateWell = "update dbo.Well set UniqueName =@UniqueName, Latitude = @Latitude, Longitude = @Longitude," +
                "CreatedAt = @CreatedAt, UpdatedAt = @UpdatedAt, PlatformId = @PlatformId where Id = @Id";

            try
            {
                int p = 0, q = 0;

                foreach (PlatformModel platform in listPlatform)
                {
                    foreach (WellModel well in platform.Well)
                    {
                        var well_ = await GetSingleData.Well(well.Id);

                        well.CreatedAt = (well.CreatedAt.HasValue) ? well.CreatedAt : well_.CreatedAt;
                        well.UpdatedAt = (well.UpdatedAt.HasValue) ? well.UpdatedAt : well_.UpdatedAt;

                        q += await connection.ExecuteAsync(sqlUpdateWell, well);
                    }

                    var platform_ = await GetSingleData.Platform(platform.Id);

                    platform.CreatedAt = (platform.CreatedAt.HasValue) ? platform.CreatedAt : platform_.CreatedAt;
                    platform.UpdatedAt = (platform.UpdatedAt.HasValue) ? platform.UpdatedAt : platform_.UpdatedAt;

                    p += await connection.ExecuteAsync(sqlUpdatePlatform, platform);
                }

                StandardMessage.RowAffectedMessage(p, q, "update");
            }
            catch (Exception ex)
            {
                StandardMessage.InsertErrorMessage(ex.Message.ToString());
            }
        }
    }
}
