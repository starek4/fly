using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using FlyApi.Enums;
using FlyApi.Exceptions;
using FlyApi.Mappers;
using FlyApi.PostModels;
using FlyApi.ResponseModels;
using Newtonsoft.Json;
using Logger.Enviroment;
using Logger.Logging;

namespace FlyApi
{
    public class Client
    {
        private readonly ILogger _logger = EnviromentHelper.GetLogger();
        private readonly PostRequest _requestHandler = new PostRequest();
        private readonly HttpClient _client = new HttpClient();

        public async Task<bool> VerifyUserLogin(string login, string password)
        {
            var postData = new VerifyUserLoginPostData(login, password);
            var apiPath = ApiPathMapper.GetPath(ApiPaths.VerifyUserLogin);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, postData.Data);
            VerifyUserLoginResponse response = Convert<VerifyUserLoginResponse>(httpContent);
            CheckResponse(response);
            if (response.Valid)
            {
                _logger?.Info("Successfully logged in.");
                return true;
            }
            _logger?.Error("Failed to log in - invalid credentials.");
            return false;
        }

        public async Task<bool> VerifyUserLoginSecuredPassword(string login, SecureString password)
        {
            return await VerifyUserLogin(login, new NetworkCredential(String.Empty, password).Password);
        }

        public async Task ClearAction(string deviceId, ApiAction action)
        {
            var data = new ClearActionPostData(deviceId, action).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.ClearAction);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task SetAction(string deviceId, ApiAction action)
        {
            var data = new SetActionPostData(deviceId, action).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.SetAction);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> GetAction(string deviceId, ApiAction action)
        {
            var data = new GetActionPostData(deviceId, action).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetAction);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            GetActionResponse response = Convert<GetActionResponse>(httpContent);
            CheckResponse(response);
            if (response.Action)
            {
                _logger?.Info("Received shutdown message.");
                return true;
            }
            return false;
        }

        public async Task ClearLoggedState(string deviceId)
        {
            var data = new ClearLoggedStatePostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.ClearLoggedState);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task SetLoggedState(string deviceId)
        {
            var data = new SetLoggedStatePostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.SetLoggedState);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<GetLoggedStateResponse> GetLoggedState(string deviceId)
        {
            var data = new GetLoggedStatePostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetLoggedState);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            GetLoggedStateResponse response = Convert<GetLoggedStateResponse>(httpContent);
            CheckResponse(response);
            if (response.Logged)
            {
                _logger?.Info("Device is already logged.");
            }
            return response;
        }

        public async Task AddDevice(string login, string deviceId, string name, bool actionable)
        {
            var data = new AddDevicePostData(deviceId, login, name, actionable).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.AddDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> VerifyDeviceId(string deviceId)
        {
            var data = new VerifyDeviceIdPostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.VerifyDeviceId);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            VerifyDeviceIdResponse response = Convert<VerifyDeviceIdResponse>(httpContent);
            CheckResponse(response);
            if (response.IsRegistered)
            {
                _logger?.Info("Device already registered - skipping registration.");
                return true;
            }
            _logger?.Info("Device is not yet registered - proceed to register.");
            return false;
        }

        public async Task<IEnumerable<Device>> GetDevices(string login)
        {
            var data = new GetDevicesPostData(login).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetDevices);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            ListDevicesResponse response = Convert<ListDevicesResponse>(httpContent);
            CheckResponse(response);
            return response.Devices;
        }

        public async Task DeleteDevice(string login, string deviceId)
        {
            var data = new DeleteDevicePostData(deviceId, login).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.DeleteDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> GetFavourite(string deviceId)
        {
            var data = new GetFavouritePostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetFavourite);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            GetFavouriteResponse response = Convert<GetFavouriteResponse>(httpContent);
            CheckResponse(response);
            return response.Favourite;
        }

        public async Task ClearFavourite(string deviceId)
        {
            var data = new ClearFavouritePostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.ClearFavourite);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task SetFavourite(string deviceId)
        {
            var data = new SetFavouritePostData(deviceId).Data;
            var apiPath = ApiPathMapper.GetPath(ApiPaths.SetFavourite);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        private void CheckResponse(BaseResponse response)
        {
            if (!response.Success)
            {
                _logger?.Error("API error: " + response.Error);
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
