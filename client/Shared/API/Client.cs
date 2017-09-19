using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RichardSzalay.MockHttp;
using Shared.API.Enums;
using Shared.API.Exceptions;
using Shared.API.Mappers;
using Shared.API.Mock;
using Shared.API.PostModels;
using Shared.API.ResponseModels;
using Shared.Enviroment;
using Shared.Logging;

namespace Shared.API
{
    public class Client
    {
        private readonly ILogger logger = EnviromentHelper.GetLogger();
        private readonly PostRequest requestHandler = new PostRequest();
        private readonly HttpClient client;

        public Client(bool mock = false)
        {
            if (mock)
            {
                var mockHttp = new MockHttpMessageHandler();
                ApiResponses.FillRules(mockHttp);
                client = mockHttp.ToHttpClient();
            }
            else
            {
                client = new HttpClient();
            }
        }

        public async Task<bool> VerifyUserLogin(string login, string password)
        {
            var data = new VerifyUserLoginPostData(login, password).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.VerifyUserLogin);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            VerifyUserLoginResponse response = Convert<VerifyUserLoginResponse>(httpContent);
            CheckResponse(response);
            if (response.Valid)
            {
                logger.Info("Successfully logged in.");
                return true;
            }
            logger.Error("Failed to log in - invalid credentials.");
            return false;
        }

        public async Task ClearShutdownPending(string deviceId)
        {
            var data = new ClearShutdownPendingPostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.ClearShutdownPending);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task SetShutdownPending(string deviceId)
        {
            var data = new SetShutdownPendingPostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.SetShutdownPending);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> GetShutdownPending(string deviceId, string login)
        {
            var data = new GetShutdownPendingPostData(deviceId, login).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetShutdownPending);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            GetShutdownPendingResponse response = Convert<GetShutdownPendingResponse>(httpContent);
            CheckResponse(response);
            if (response.Shutdown)
            {
                logger.Info("Received shutdown message.");
                return true;
            }
            return false;
        }

        public async Task AddDevice(string login, string deviceId, string name)
        {
            var data = new AddDevicePostData(deviceId, login, name).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.AddDevice);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> VerifyDeviceId(string deviceId)
        {
            var data = new VerifyDeviceIdPostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.VerifyDeviceId);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            VerifyDeviceIdResponse response = Convert<VerifyDeviceIdResponse>(httpContent);
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
            var data = new GetDevicesPostData(login).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetDevices);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            ListDevicesResponse response = Convert<ListDevicesResponse>(httpContent);
            CheckResponse(response);
            return response.Devices;
        }

        public async Task DeleteDevice(string login, string deviceId)
        {
            var data = new DeleteDevicePostData(deviceId, login).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.DeleteDevice);
            var httpContent = await requestHandler.DoRequest(client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        private void CheckResponse(BaseResponse response)
        {
            if (!response.Success)
            {
                logger.Error("API error: " + response.Error);
                throw new DatabaseException("Database error");
            }
        }

        private T Convert<T>(string httpContent)
        {
            // This code can generate exception JsonException which have to be catched!
            return JsonConvert.DeserializeObject<T>(httpContent);
        }
    }
}
