using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SyncApp.Models.ViewModels;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SyncApp.Services
{
    public class LoginService : ILoginService
    {
        private readonly ILogger<LoginService> logger;
        private string BASE_API = Constants.API.BASE_API;

        public LoginService(ILogger<LoginService> logger)
        {
            this.logger = logger;
        }

        public async Task<string> Login(LoginViewModel model)
        {
            try
            {
                using (var client = new HttpClient())
                {

                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync($"{BASE_API}account/login", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var token = JsonConvert.DeserializeObject<string>(apiResponse);

                        logger.LogInformation("Success call login api");

                        return token;
                    }
                }
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Failed call login api");
                return null;
            }
           
        }
    }
}
