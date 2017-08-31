using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.API.Exceptions;
using Shared.API.Models;
using Shared.Logging;

namespace Shared.API
{
    public class Client
    {
        private async Task<string> Post(string apiPath, Dictionary<string, string> body)
        {
            var baseAddress = new Uri("https://fly.starekit.cz/api/" + apiPath);
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var content = new FormUrlEncodedContent(body))
                {
                    try
                    {
                        using (var response = await httpClient.PostAsync(baseAddress, content))
                        {
                            return await response.Content.ReadAsStringAsync();
                        }
                    }
                    catch (HttpRequestException exception)
                    {
                        Logger.Error("Error when connecting server: " + exception);
                        throw new HttpRequestException("Error when communucating with network...");
                    }
                }
            }
        }

        public async Task<bool> VerifyUserLogin(string login, string password)
        {
            var httpContent = await Post("verifyUserLogin.php", new Dictionary<string, string> { { "Login", login }, { "Password", password } });
            VerifyUserLoginResponse response = JsonConvert.DeserializeObject<VerifyUserLoginResponse>(httpContent);
            CheckResponse(response);
            if (response.Valid)
            {
                Logger.Info("Successfully logged in.");
                return true;
            }
            Logger.Error("Failed to log in - invalid credentials.");
            return false;
        }

        public async void ClearShutdownPending(string deviceId)
        {
            var httpContent = await Post("clearShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async void SetShutdownPending(string deviceId)
        {
            var httpContent = await Post("setShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> GetShutdownPending(string deviceId, string login)
        {
            var httpContent = await Post("getShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId }, { "Login", login } });
            GetShutdownPendingResponse response = JsonConvert.DeserializeObject<GetShutdownPendingResponse>(httpContent);
            CheckResponse(response);
            if (response.Shutdown)
            {
                Logger.Info("Received shutdown message.");
                return true;
            }
            return false;
        }

        public async void AddDevice(string login, string deviceId, string name)
        {
            var httpContent = await Post("addDevice.php", new Dictionary<string, string> { { "Login", login }, { "Device_id", deviceId }, { "Name", name } });
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> VerifyDeviceId(string deviceId)
        {
            var httpContent = await Post("verifyDeviceId.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            VerifyDeviceIdResponse response = JsonConvert.DeserializeObject<VerifyDeviceIdResponse>(httpContent);
            CheckResponse(response);
            if (response.IsRegistered)
            {
                Logger.Info("Device already registered - skipping registration.");
                return true;
            }
            Logger.Info("Device is not yet registered - proceed to register.");
            return false;
        }

        public async Task<IEnumerable<Device>> GetDevices(string login)
        {
            var httpContent = await Post("getDevices.php", new Dictionary<string, string> { { "Login", login } });
            ListDevicesResponse response = JsonConvert.DeserializeObject<ListDevicesResponse>(httpContent);
            CheckResponse(response);
            return response.Devices;
        }

        private void CheckResponse(BaseResponse response)
        {
            if (!response.Success)
            {
                Logger.Error("API error: " + response.Error);
                throw new DatabaseException("Database error");
            }
        }
    }
}
