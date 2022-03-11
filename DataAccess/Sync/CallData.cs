using Authentication.Service;
using Newtonsoft.Json;
using Common.Constants;
using Common.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common;

namespace SyncDataProcess.Sync
{
    public class CallData
    {
        private static readonly IAuthService authService = new AuthService();

        public static async Task<List<PlatformModel>> Retrieve()
        {
            string type = CaptureUrlType.UrlType();
            string url = (type.Equals("Y") ? API.GET_PLATFORM_WELL_ACTUAL : API.GET_PLATFORM_WELL_DUMMY);

            string token = await authService.Login();

            if (!string.IsNullOrWhiteSpace(token))
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                    using (var response = await client.GetAsync($"{API.API_URL}{url}"))
                    {
                        try
                        {
                            StandardMessage.FetchMessage();

                            if (response.IsSuccessStatusCode)
                            {
                                string apiResponse = await response.Content.ReadAsStringAsync();
                                var result = JsonConvert.DeserializeObject<List<PlatformModel>>(apiResponse);

                                StandardMessage.FetchSuccessMessage();

                                return result;
                            }
                        }
                        catch (Exception ex)
                        {
                            StandardMessage.FetchErrorMessage(ex.Message.ToString());
                            return new List<PlatformModel>();
                        }

                    }

                }
            }

            StandardMessage.FetchErrorMessage("Token empty or not valid.");
            return new List<PlatformModel>();
        }

       

    }
}
