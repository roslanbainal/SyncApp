using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SyncApp.Data;
using SyncApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SyncApp.Services
{
    public class SyncService : ISyncService
    {
        private readonly ILogger<SyncService> logger;
        private readonly AppDbContext context;
        private string BASE_API = Constants.API.BASE_API;

        public SyncService(ILogger<SyncService> logger, AppDbContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public async Task<bool> GetDataTask(string token)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    using (var response = await client.GetAsync($"{BASE_API}PlatformWell/GetPlatformWellActual"))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            string apiResponse = await response.Content.ReadAsStringAsync();
                            var result = JsonConvert.DeserializeObject<List<Platform>>(apiResponse);

                            logger.LogInformation("Success get data");

                            bool successOrNot = await SyncDataTask(result);

                            return successOrNot = true ? true : false;

                        }

                        logger.LogError("", "Failed get data");
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed get data");
                return false;
            }
        }

        public async Task<bool> SyncDataTask(List<Platform> listPlatform)
        {
            try
            {
                //Platform
                foreach (Platform platform in listPlatform)
                {
                    var checkPlatformExist = await context.Platform.FirstOrDefaultAsync(x => x.Id == platform.Id);

                    //if platform not exist yet
                    if (checkPlatformExist == null)
                    {
                        var newPlatform = new Platform();
                        newPlatform.Id = platform.Id;
                        newPlatform.UniqueName = platform.UniqueName;
                        newPlatform.Latitude = platform.Latitude;
                        newPlatform.Longitude = platform.Longitude;
                        newPlatform.CreatedAt = platform.CreatedAt;
                        newPlatform.UpdatedAt = platform.UpdatedAt;
                        newPlatform.Well = null;

                        await context.Platform.AddAsync(newPlatform);
                        await context.SaveChangesAsync();

                    }
                    else
                    {
                        checkPlatformExist.Id = platform.Id;
                        checkPlatformExist.UniqueName = platform.UniqueName;
                        checkPlatformExist.Latitude = platform.Latitude;
                        checkPlatformExist.Longitude = platform.Longitude;
                        checkPlatformExist.CreatedAt = platform.CreatedAt;
                        checkPlatformExist.UpdatedAt = platform.UpdatedAt;
                        await context.SaveChangesAsync();

                    }


                    //Well
                    foreach (Well well in platform.Well)
                    {
                        var checkWellExist = await context.Well.FirstOrDefaultAsync(x => x.Id == well.Id);

                        //if well not exist
                        if (checkWellExist == null)
                        {
                            var newWell = new Well();
                            newWell.Id = well.Id;
                            newWell.PlatformId = well.PlatformId;
                            newWell.UniqueName = well.UniqueName;
                            newWell.Latitude = well.Latitude;
                            newWell.Longitude = well.Longitude;
                            newWell.CreatedAt = well.CreatedAt;
                            newWell.UpdatedAt = well.UpdatedAt;

                            await context.Well.AddAsync(newWell);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            checkWellExist.Id = well.Id;
                            checkWellExist.PlatformId = well.PlatformId;
                            checkWellExist.UniqueName = well.UniqueName;
                            checkWellExist.Latitude = well.Latitude;
                            checkWellExist.Longitude = well.Longitude;
                            checkWellExist.CreatedAt = well.CreatedAt;
                            checkWellExist.UpdatedAt = well.UpdatedAt;
                            await context.SaveChangesAsync();
                        }

                    }
                }

                return true;

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error save data to db");
                return false;
            }
        }
    }
}
