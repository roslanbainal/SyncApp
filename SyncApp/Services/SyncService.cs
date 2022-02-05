using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SyncApp.Data;
using SyncApp.Models;
using SyncApp.Models.ViewModels;
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
        private readonly IMapper mapper;
        private string BASE_API = Constants.API.BASE_API;

        public SyncService(ILogger<SyncService> logger, AppDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<bool> GetDataAndSyncTask(string token)
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
                            var result = JsonConvert.DeserializeObject<List<PlatformViewModel>>(apiResponse);

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

        public async Task<bool> SyncDataTask(List<PlatformViewModel> listPlatform)
        {
            try
            {
                //Platform
                foreach (PlatformViewModel platform in listPlatform)
                {
                    //check if platform id exist
                    var checkPlatformExist = await context.Platform.FindAsync(platform.Id);

                    //if platform not exist yet
                    if (checkPlatformExist == null)
                    {
                        var data = mapper.Map(platform,new Platform());

                        await context.Platform.AddAsync(data);
                        await context.SaveChangesAsync();

                    }
                    else
                    {
                        platform.CreatedAt = (!platform.CreatedAt.HasValue) ? checkPlatformExist.CreatedAt : platform.CreatedAt;

                        mapper.Map(platform, checkPlatformExist);   
                        await context.SaveChangesAsync();

                    }

                    //Well
                    foreach (WellViewModel well in platform.Well)
                    {
                        //check if well id exist
                        var checkWellExist = await context.Well.FindAsync(well.Id);

                        //if well not exist
                        if (checkWellExist == null)
                        {

                            var data = mapper.Map(well, new Well());

                            await context.Well.AddAsync(data);
                            await context.SaveChangesAsync();
                        }
                        else
                        {
                            well.CreatedAt = (!well.CreatedAt.HasValue) ? checkWellExist.CreatedAt : well.CreatedAt;

                            mapper.Map(well, checkWellExist);
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
