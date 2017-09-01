using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared.API.Exceptions;
using Shared.API.Models;
using Shared.Enviroment;
using Shared.Logging;

namespace Shared.API
{
    public class Client
    {
        private readonly ILogger logger;

        public Client()
        {
            logger = EnviromentHelper.GetLogger();
        }

        public async Task<bool> VerifyUserLogin(string login, string password)
        {
            var httpContent = await PostRequest.DoRequest("verifyUserLogin.php", new Dictionary<string, string> { { "Login", login }, { "Password", password } });
            VerifyUserLoginResponse response = JsonConvert.DeserializeObject<VerifyUserLoginResponse>(httpContent);
            CheckResponse(response);
            if (response.Valid)
            {
                logger.Info("Successfully logged in.");
                return true;
            }
            logger.Error("Failed to log in - invalid credentials.");
            return false;
        }

        public async void ClearShutdownPending(string deviceId)
        {
            var httpContent = await PostRequest.DoRequest("clearShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async void SetShutdownPending(string deviceId)
        {
            var httpContent = await PostRequest.DoRequest("setShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> GetShutdownPending(string deviceId, string login)
        {
            var httpContent = await PostRequest.DoRequest("getShutdownPending.php", new Dictionary<string, string> { { "Device_id", deviceId }, { "Login", login } });
            GetShutdownPendingResponse response = JsonConvert.DeserializeObject<GetShutdownPendingResponse>(httpContent);
            CheckResponse(response);
            if (response.Shutdown)
            {
                logger.Info("Received shutdown message.");
                return true;
            }
            return false;
        }

        public async void AddDevice(string login, string deviceId, string name)
        {
            var httpContent = await PostRequest.DoRequest("addDevice.php", new Dictionary<string, string> { { "Login", login }, { "Device_id", deviceId }, { "Name", name } });
            BaseResponse response = JsonConvert.DeserializeObject<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> VerifyDeviceId(string deviceId)
        {
            var httpContent = await PostRequest.DoRequest("verifyDeviceId.php", new Dictionary<string, string> { { "Device_id", deviceId } });
            VerifyDeviceIdResponse response = JsonConvert.DeserializeObject<VerifyDeviceIdResponse>(httpContent);
            CheckResponse(response);
            if (response.IsRegistered)
            {
                logger.Info("Device already registered - skipping registration.");
                return true;
            }
            logger.Info("Device is not yet registered - proceed to register.");
            return false;
        }

        public async Task<IEnumerable<Device>> GetDevices(string login)
        {
            var httpContent = await PostRequest.DoRequest("getDevices.php", new Dictionary<string, string> { { "Login", login } });
            ListDevicesResponse response = JsonConvert.DeserializeObject<ListDevicesResponse>(httpContent);
            CheckResponse(response);
            return response.Devices;
        }

        private void CheckResponse(BaseResponse response)
        {
            if (!response.Success)
            {
                logger.Error("API error: " + response.Error);
                throw new DatabaseException("Database error");
            }
        }
    }
}
