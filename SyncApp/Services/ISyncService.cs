using SyncApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncApp.Services
{
    public interface ISyncService
    {
        Task<bool> GetDataTask(string token);
        Task<bool> SyncDataTask(List<Platform> listPlatform);
    }
}
