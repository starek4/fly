using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
                        return String.Empty;
                    }
                }
            }
        }

        public async Task<bool> VerifyUserLogin(string login, string password)
        {
            var httpContent = await Post("verifyUserLogin.php", new Dictionary<string, string> { { "Login", login }, { "Password", password } });
            if (httpContent == String.Empty)
                throw new HttpRequestException("Error when connecting server.");

            VerifyUserLoginResponse response = JsonConvert.DeserializeObject<VerifyUserLoginResponse>(httpContent);
            if (response.Success && response.Valid)
            {
                Logger.Info("Successfully logged in.");
                return true;
            }
            else if (response.Success && response.Valid == false)
            {
                Logger.Error("Failed to log in - invalid credentials.");
                return false;
            }
            else
            {
                Logger.Error("Failed to log in - bad request or server unreachable.");
                throw new HttpRequestException("Database error");
            }
        }

        public async Task<bool> ClearShutdownPending(string deviceId)
        {
            var httpContent = await Post("clearShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            if (httpContent == String.Empty)
                throw new HttpRequestException("Error when connecting server.");
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            if (response.Success)
                return true;
            Logger.Error("Failed to clear pending shutdown.");
            Logger.Error("API error: " + response.Error);
            throw new HttpRequestException("Database error");
        }

        public async Task<bool> SetShutdownPending(string deviceId)
        {
            var httpContent = await Post("setShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            if (httpContent == String.Empty)
                throw new HttpRequestException("Error when connecting server.");
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            if (response.Success)
                return true;
            Logger.Error("Failed to set pending shutdown.");
            Logger.Error("API error: " + response.Error);
            throw new HttpRequestException("Database error");
        }

        public async Task<bool> GetShutdownPending(string deviceId, string login)
        {
            var httpContent = await Post("getShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId }, { "Login", login } });
            if (httpContent == String.Empty)
                throw new HttpRequestException("Error when connecting server.");
            GetShutdownPendingResponse response = JsonConvert.DeserializeObject<GetShutdownPendingResponse>(httpContent);
            if (response.Success)
            {
                if (response.Shutdown)
                {
                    Logger.Info("Received shutdown message.");
                    return true;
                }
                return false;
            }
            Logger.Error("Failed to get pending shutdown.");
            Logger.Error("API error: " + response.Error);
            throw new HttpRequestException("Database error");
        }

        public async Task<bool> AddDevice(string login, string deviceId, string name)
        {
            var httpContent = await Post("addDevice.php", new Dictionary<string, string> { { "Login", login }, { "Device_id", deviceId }, { "Name", name } });
            if (httpContent == String.Empty)
                throw new HttpRequestException("Error when connecting server.");
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            if (response.Success)
            {
                Logger.Info("New device successfully registered: " + name);
                return true;
            }
            Logger.Error("Failed to register device: " + name);
            Logger.Error("API error: " + response.Error);
            throw new HttpRequestException("Database error");
        }

        public async Task<bool> VerifyDeviceId(string deviceId)
        {
            var httpContent = await Post("verifyDeviceId.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            if (httpContent == String.Empty)
                throw new HttpRequestException("Error when connecting server.");
            VerifyDeviceIdResponse response = JsonConvert.DeserializeObject<VerifyDeviceIdResponse>(httpContent);
            if (response.Success && response.IsRegistered)
            {
                Logger.Info("Device already registered - skipping registration.");
                return true;
            }
            if (response.Success && response.IsRegistered == false)
            {
                Logger.Info("Device is not yet registered - proceed to register.");
                return false;
            }
            Logger.Info("Cannot verify device registration - bad request or server unreachable.");
            Logger.Error("API error: " + response.Error);
            throw new HttpRequestException("Database error");
        }

        public async Task<IEnumerable<Device>> GetDevices(string login)
        {
            var httpContent = await Post("getDevices.php", new Dictionary<string, string> { { "Login", login } });
            if (httpContent == String.Empty)
                throw new HttpRequestException("Error when connecting server.");
            ListDevicesResponse response = JsonConvert.DeserializeObject<ListDevicesResponse>(httpContent);
            if (response.Success)
            {
                return response.Devices;
            }
            Logger.Error("Failed to get devices");
            Logger.Error("API error: " + response.Error);
            throw new HttpRequestException("Database error");
        }
    }
}