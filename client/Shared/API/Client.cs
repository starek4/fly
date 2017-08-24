using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using Shared.API.Models;

namespace Shared.API
{
    public class Client
    {
        private string Post(string apiPath, Dictionary<string, string> body)
        {
            var baseAddress = new Uri("http://fly.starekit.cz/api/" + apiPath);
            using (var httpClient = new HttpClient { BaseAddress = baseAddress })
            {
                using (var content = new FormUrlEncodedContent(body))
                {
                    using (var response = httpClient.PostAsync(baseAddress, content).Result)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                    }
                }
            }
        }

        public bool VerifyUserLogin(string login, string password)
        {
            var httpContent = Post("verifyUserLogin.php", new Dictionary<string, string> { { "Login", login }, { "Password", password } });
            VerifyUserLoginResponse content =
                JsonConvert.DeserializeObject<VerifyUserLoginResponse>(httpContent);
            if (content.Status == "pass" && content.Valid == "yes")
            {
                Logger.Logger.Info("Successfully logged in.");
                return true;
            }
            else if (content.Status == "pass" && content.Valid == "no")
            {
                Logger.Logger.Error("Failed to log in - invalid credentials.");
                return false;
            }
            else
            {
                Logger.Logger.Error("Failed to log in - bad request or server unreachable.");
                return false;
            }
        }

        public bool ClearShutdownPending(string deviceId)
        {
            var httpContent = Post("clearShutdownPending.php",
                new Dictionary<string, string> { { "Device_id", deviceId } });
            ChangeShutdownPendingResponse content =
                JsonConvert.DeserializeObject<ChangeShutdownPendingResponse>(httpContent);
            if (content.Status == "yes")
                return true;
            Logger.Logger.Error("Failed to clear pending shutdown.");
            return false;
        }

        public bool SetShutdownPending(string deviceId)
        {
            var httpContent = Post("setShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            ChangeShutdownPendingResponse content =
                JsonConvert.DeserializeObject<ChangeShutdownPendingResponse>(httpContent);
            if (content.Status == "yes")
                return true;
            Logger.Logger.Error("Failed to set pending shutdown.");
            return false;
        }

        public bool GetShutdownPending(string deviceId, string login)
        {
            var httpContent = Post("getShutdownPending.php",
                new Dictionary<string, string> { { "Device_id", deviceId }, { "Login", login } });
            GetShutdownPendingResponse content = JsonConvert.DeserializeObject<GetShutdownPendingResponse>(httpContent);
            if (content.Shutdown == "yes")
                return true;
            return false;
        }

        public bool AddDevice(string login, string deviceId, string name)
        {
            var httpContent = Post("addDevice.php",
                new Dictionary<string, string> { { "Login", login }, { "Device_id", deviceId }, { "Name", name } });
            AddDeviceResponse content = JsonConvert.DeserializeObject<AddDeviceResponse>(httpContent);
            if (content.Result == "yes")
            {
                Logger.Logger.Info("New device successfully registered: " + name);
                return true;
            }
            Logger.Logger.Error("Failed to register device: " + name);
            return false;
        }

        public bool VerifyDeviceId(string deviceId)
        {
            var httpContent = Post("verifyDeviceId.php",
                new Dictionary<string, string> { { "Device_id", deviceId } });
            VerifyDeviceIdResponse content = JsonConvert.DeserializeObject<VerifyDeviceIdResponse>(httpContent);
            if (content.Status == "pass" && content.IsRegistered == "yes")
            {
                Logger.Logger.Info("Device already registered - skipping registration.");
                return true;
            }
            else if (content.Status == "pass" && content.IsRegistered == "no")
            {
                Logger.Logger.Info("Device is not yet registered - proceed to register.");
                return false;
            }
            else
            {
                Logger.Logger.Info("Cannot verify device registration - bad request or server unreachable.");
                return false;
            }
        }

        public IEnumerable<Device> GetDevices(string login)
        {
            var httpContent = Post("getDevices.php",
                new Dictionary<string, string> { { "Login", login } });
            ListDevicesResponse content = JsonConvert.DeserializeObject<ListDevicesResponse>(httpContent);
            return content.Devices;
        }
    }
}