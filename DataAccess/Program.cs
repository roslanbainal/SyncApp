using Authentication.Service;
using Common.Models;
using SyncDataProcess.Sync;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SyncDataProcess
{
    public static class Program
    {
        static async Task Main(string[] args) => await SyncTask();

        public static async Task SyncTask()
        {
            List<PlatformModel> platformModels = await CallData.Retrieve();
            await SyncData.Task(platformModels);
        }
    }
}
