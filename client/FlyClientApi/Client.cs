using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using FlyClientApi.Mappers;
using FlyClientApi.Enums;
using FlyClientApi.Exceptions;
using Newtonsoft.Json;
using Logger.Enviroment;
using Logger.Logging;
using Models;
using Models.PostModels;
using Models.ResponseModels;

namespace FlyClientApi
{
    public class Client
    {
        private readonly ILogger _logger = EnviromentHelper.GetLogger();
        private readonly PostRequest _requestHandler = new PostRequest();
        private readonly HttpClient _client = new HttpClient();

        public async Task<bool> VerifyUserLogin(string login, string password, string deviceId)
        {
            string data = JsonConvert.SerializeObject(new VerifyUserPostModel { Login = login, Password = password, DeviceId = deviceId});
            var apiPath = ApiPathMapper.GetPath(ApiPaths.VerifyUser);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            VerifyUserResponseModel response = Convert<VerifyUserResponseModel>(httpContent);
            CheckResponse(response);
            if (response.IsVerified)
            {
                _logger?.Info("Successfully logged in.");
                return true;
            }
            _logger?.Error("Failed to log in - invalid credentials.");
            return false;
        }
        
        public async Task<bool> VerifyUserLoginSecuredPassword(string login, SecureString password, string deviceId)
        {
            return await VerifyUserLogin(login, new NetworkCredential(String.Empty, password).Password, deviceId);
        }

        public async Task<Device> GetDevice(string deviceId)
        {
            string data = JsonConvert.SerializeObject(new GetDevicePostModel { DeviceId = deviceId });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            GetDeviceResponseModel response = Convert<GetDeviceResponseModel>(httpContent);
            CheckResponse(response);
            return response.Device;
        }

        public async Task<string> GetUsername(string deviceId)
        {
            if (!await VerifyDeviceId(deviceId))
                return null;

            string data = JsonConvert.SerializeObject(new GetUsernamePostModel { DeviceId = deviceId });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetUsername);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            GetUsernameResponseModel response = Convert<GetUsernameResponseModel>(httpContent);
            CheckResponse(response);
            return response.Username;
        }

        public async Task<bool> VerifyDeviceId(string deviceId)
        {
            return await GetDevice(deviceId) != null;
        }

        private async Task SetClearAction(string deviceId, Actions action, bool setOrClear)
        {
            Device device = await GetDevice(deviceId);
            ActionMapper.ActionSet(action, device, setOrClear);

            string data = JsonConvert.SerializeObject(new UpdateDevicePostModel { Device = device });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.UpdateDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }


        public async Task ClearAction(string deviceId, Actions action)
        {
            await SetClearAction(deviceId, action, false);
        }
        
        public async Task SetAction(string deviceId, Actions action)
        {
            await SetClearAction(deviceId, action, true);
        }

        public async Task<bool> GetAction(string deviceId, Actions action)
        {
            Device device = await GetDevice(deviceId);
            return ActionMapper.ActionGet(action, device);
        }

        public async Task SetLoggedState(string deviceId, bool setOrClear)
        {
            Device device = await GetDevice(deviceId);
            device.IsLogged = setOrClear;

            string data = JsonConvert.SerializeObject(new UpdateDevicePostModel { Device = device });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.UpdateDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> GetLoggedState(string deviceId)
        {
            Device device = await GetDevice(deviceId);
            if (device == null)
                return false;
            if (device.IsLogged)
            {
                _logger?.Info("Device is already logged.");
            }
            return device.IsLogged;
        }

        public async Task AddDevice(string login, string deviceId, string name, bool actionable)
        {
            string data = JsonConvert.SerializeObject(new AddDevicePostModel { Login = login, DeviceId = deviceId, Name = name, Actionable = actionable });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.AddDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task AddUser(string login, string password, string email)
        {
            string data = JsonConvert.SerializeObject(new AddUserPostModel { Login = login, Password = password, Mail = email });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.AddUser);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<List<Device>> GetDevices(string login)
        {
            string data = JsonConvert.SerializeObject(new GetDevicesByLoginPostModel { Login = login });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.GetDevicesByLogin);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            GetDevicesByLoginResponseModel response = Convert<GetDevicesByLoginResponseModel>(httpContent);
            CheckResponse(response);
            return response.Devices;
        }

        public async Task DeleteDevice(string deviceId)
        {
            string data = JsonConvert.SerializeObject(new DeleteDevicePostModel { DeviceId = deviceId });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.DeleteDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task<bool> GetFavourite (string deviceId)
        {
            Device device = await GetDevice(deviceId);
            return device.IsFavourite;
        }

        private async Task SetFavourite(string deviceId, bool setOrClear)
        {
            Device device = await GetDevice(deviceId);
            device.IsFavourite = setOrClear;

            string data = JsonConvert.SerializeObject(new UpdateDevicePostModel { Device = device });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.UpdateDevice);
            var httpContent = await _requestHandler.DoRequest(_client, apiPath, data);
            BaseResponse response = Convert<BaseResponse>(httpContent);
            CheckResponse(response);
        }

        public async Task ClearFavourite(string deviceId)
        {
            await SetFavourite(deviceId, false);
        }

        public async Task SetFavourite(string deviceId)
        {
            await SetFavourite(deviceId, true);
        }

        public async Task UpdateTimestamp(string deviceId)
        {
            Device device = await GetDevice(deviceId);
            device.LastActive = DateTime.Now;

            string data = JsonConvert.SerializeObject(new UpdateDevicePostModel { Device = device });
            var apiPath = ApiPathMapper.GetPath(ApiPaths.UpdateDevice);
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
