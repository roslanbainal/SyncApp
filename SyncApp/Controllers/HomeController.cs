using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SyncApp.Models;
using SyncApp.Models.ViewModels;
using SyncApp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SyncApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService loginService;
        private readonly ISyncService syncService;

        public HomeController(ILoginService loginService, ISyncService syncService)
        {
            this.loginService = loginService;
            this.syncService = syncService;
        }

        public IActionResult SyncData()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SyncData(LoginViewModel model)
        {

            var token= await loginService.Login(model);

            bool success = await syncService.GetDataAndSyncTask(token);

            if(success)
            {
                TempData["Message"] = "Success Sync Data";
                return RedirectToAction("SyncData");
            }

            TempData["Message"] = "Failed Sync Data";
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
