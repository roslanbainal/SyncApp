using SyncApp.Models;
using SyncApp.Models.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncApp.Services
{
    public interface ISyncService
    {
        Task<bool> GetDataAndSyncTask(string token);
        Task<bool> SyncDataTask(List<PlatformViewModel> listPlatform);
    }
}
