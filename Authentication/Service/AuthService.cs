
using Common;
using Common.Constants;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Service
{
    public class AuthService : IAuthService
    {
        public async Task<string> Login()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    StandardMessage.StartMessage();

                    LoginModel model = new LoginModel();

                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    using (var response = await client.PostAsync($"{API.API_URL}{API.LOGIN}", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        var token = JsonConvert.DeserializeObject<string>(apiResponse);

                        StandardMessage.SuccessLoginMessage(token);
                        return token;
                    }

                }
            }
            catch (Exception ex)
            {
                StandardMessage.ErrorLoginMessage(ex.Message.ToString());
                return null;
            }

        }
    }
}
